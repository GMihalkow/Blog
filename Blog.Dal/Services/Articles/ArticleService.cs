using Blog.Dal.Infrastructure.Constants;
using Blog.Dal.Infrastructure.Extensions;
using Blog.Dal.Models.Article;
using Blog.Dal.Models.Article.Contracts;
using Blog.Dal.Models.Common;
using Blog.Dal.Services.Articles.Contracts;
using Blog.Data;
using Blog.Models;
using Ganss.XSS;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;

namespace Blog.Dal.Services.Articles
{
    public class ArticleService : IArticleService
    {
        private readonly BlogDbContext _dbContext;

        public ArticleService(BlogDbContext dbContext)
             => this._dbContext = dbContext;

        public async Task<IEnumerable<ArticleIndexModel>> GetPopularArticles(int count) 
            => await this.GetMappedArticles(3,
            (a) => new ArticleIndexModel
            {
                Id = a.Id,
                Title = a.Title,
                CoverUrl = a.CoverUrl,
                CreatedOn = a.CreatedOn
            },
            (a) => a.ViewsCount);

        public async Task<IEnumerable<ArticleIndexModel>> GetLatestArticles(int count)
           => await this.GetMappedArticles(3,
           (a) => new ArticleIndexModel
           {
               Id = a.Id,
               Title = a.Title,
               CoverUrl = a.CoverUrl,
               CreatedOn = a.CreatedOn
           },
           (a) => a.CreatedOn);

        public async Task<ArticleViewModel> GetById(string id)
        {
            var article = await this._dbContext.Articles.Include((a) => a.Creator).FirstOrDefaultAsync((a) => a.Id == id);

            if (article == null) return null;

            var articleViewModel = new ArticleViewModel
            {
                Id = article.Id,
                Title = article.Title,
                Content = article.Content,
                CoverUrl = article.CoverUrl,
                CategoryId = article.CategoryId,
                CreatorId = article.CreatorId,
                CreatorName = article.Creator?.UserName ?? "No Author",
                CreatedOn = article.CreatedOn,
                ViewsCount = article.ViewsCount
            };

            return articleViewModel;
        }

        public async Task<ISearchResponseModel<ArticleSearchViewModel>> GetAll(IArticleSearchModel searchModel)
        {
            var filteredArticles = await this._dbContext
                .Articles
                .Where(a => searchModel.Keywords.IsNullOrEmpty() || a.Title.StartsWith(searchModel.Keywords))
                .Where(a => searchModel.CategoryId.IsNullOrEmpty() || a.CategoryId == searchModel.CategoryId)
                .Where(a => searchModel.CreatorId.IsNullOrEmpty() || a.CreatorId == searchModel.CreatorId)
                .Select(a => new ArticleSearchViewModel
                {
                    Id = a.Id,
                    Title = a.Title,
                    CategoryName = a.Category != null ? a.Category.Name : "None",
                    CreatedOn = a.CreatedOn,
                    CreatorId = a.CreatorId,
                    CreatorName = a.Creator != null ? a.Creator.UserName : "None"
                })
                .ToListAsync();

            var data = filteredArticles
                .OptimizedSkip((searchModel.Page.Value - 1) * searchModel.Size.Value)
                .Take(searchModel.Size.Value);

            var pagesCount = (double)(filteredArticles.Count) / (double)searchModel.Size.Value;
            var responseModel = new SearchResponseModel<ArticleSearchViewModel>(data, (int)Math.Ceiling(pagesCount));

            return responseModel;
        }

        public async Task Create(ArticleInputModel model)
        {
            var sanitizer = new HtmlSanitizer(DalConstants.AllowedHtmlTags);
            var decodedContent = HttpUtility.HtmlDecode(model.Content);

            var article = new Article
            {
                CategoryId = model.CategoryId,
                Content = sanitizer.Sanitize(decodedContent),
                Title = model.Title,
                CreatorId = model.CreatorId,
                CoverUrl = model.CoverUrl
            };

            await this._dbContext.Articles.AddAsync(article);
            await this._dbContext.SaveChangesAsync();
        }

        public async Task Edit(ArticleEditModel model)
        {
            var article = await this._dbContext.Articles.FirstOrDefaultAsync(a => a.Id == model.Id);
            if (article == null) return;

            var sanitizer = new HtmlSanitizer(DalConstants.AllowedHtmlTags);
            var decodedContent = HttpUtility.HtmlDecode(model.Content);

            article.Title = model.Title;
            article.CategoryId = model.CategoryId;
            article.Content = sanitizer.Sanitize(decodedContent);
            article.CoverUrl = model.CoverUrl;

            this._dbContext.Update(article);
            await this._dbContext.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            var article = await this._dbContext.Articles.FirstOrDefaultAsync(a => a.Id == id);
            if (article == null) return;

            this._dbContext.Articles.Remove(article);
            await this._dbContext.SaveChangesAsync();
        }

        public void IncrementViews(string articleId)
        {
            var article = this._dbContext.Articles.FirstOrDefault((a) => a.Id == articleId);
            if (article == null) return;

            article.ViewsCount++;

            this._dbContext.Articles.Update(article);
            this._dbContext.SaveChanges();
        }

        private async Task<IEnumerable<T>> GetMappedArticles<T>(int count, Expression<Func<Article, T>> mapFunc, Expression<Func<Article, object>> orderByFunc)
            => await this._dbContext.Articles
                .OrderByDescending(orderByFunc)
                .Take(count)
                .Select(mapFunc)
                .ToListAsync();
    }
}
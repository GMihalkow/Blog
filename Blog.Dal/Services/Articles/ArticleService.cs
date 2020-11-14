using Blog.Dal.Infrastructure.Constants;
using Blog.Dal.Infrastructure.Extensions;
using Blog.Dal.Models.Article;
using Blog.Dal.Models.Common;
using Blog.Dal.Models.Common.Contracts;
using Blog.Dal.Services.Articles.Contracts;
using Blog.Data;
using Blog.Models;
using Ganss.XSS;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Dal.Services.Articles
{
    public class ArticleService : IArticleService
    {
        private readonly BlogDbContext _dbContext;

        public ArticleService(BlogDbContext dbContext)
             => this._dbContext = dbContext;

        public async Task<ArticleViewModel> GetById(string id)
        {
            var article = await this._dbContext.Articles.FirstOrDefaultAsync(a => a.Id == id);

            if (article == null)
                return null;

            var articleViewModel = new ArticleViewModel
            {
                Id = article.Id,
                Title = article.Title,
                Content = article.Content,
                CategoryId = article.CategoryId,
                CreatorId = article.CreatorId
            };

            return articleViewModel;
        }

        public async Task<ISearchResponseModel<ArticleSearchViewModel>> GetAll(IBaseSearchModel searchModel)
        {
            var filteredArticles = await this._dbContext
                .Articles
                .Where(a => searchModel.Keywords.IsNullOrEmpty() || a.Title.StartsWith(searchModel.Keywords))
                .Select(a => new ArticleSearchViewModel
                {
                    Id = a.Id,
                    Title = a.Title,
                    CategoryId = a.CategoryId,
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

            var article = new Article
            {
                CategoryId = model.CategoryId,
                Content = sanitizer.Sanitize(model.Content),
                Title = model.Title,
                CreatorId = model.CreatorId
            };

            await this._dbContext.Articles.AddAsync(article);
            await this._dbContext.SaveChangesAsync();
        }

        public async Task Edit(ArticleEditModel model)
        {
            var article = await this._dbContext.Articles.FirstOrDefaultAsync(a => a.Id == model.Id);

            if (article == null) return;

            var sanitizer = new HtmlSanitizer(DalConstants.AllowedHtmlTags);

            article.Title = model.Title;
            article.CategoryId = model.CategoryId;
            article.Content = sanitizer.Sanitize(model.Content);

            this._dbContext.Update(article);
            await this._dbContext.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            var article = await this._dbContext.Articles.FirstOrDefaultAsync(a => a.Id == id);

            if (article != null)
            {
                this._dbContext.Articles.Remove(article);
                await this._dbContext.SaveChangesAsync();
            }
        }
    }
}
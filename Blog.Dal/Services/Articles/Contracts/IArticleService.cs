using Blog.Dal.Models.Article;
using System.Collections.Generic;
using Blog.Dal.Models.Article.Contracts;
using Blog.Dal.Models.Common;
using System.Threading.Tasks;

namespace Blog.Dal.Services.Articles.Contracts
{
    public interface IArticleService
    {
        Task<IEnumerable<ArticleIndexModel>> GetPopularArticles(int count);

        Task<IEnumerable<ArticleIndexModel>> GetLatestArticles(int count);

        Task<ArticleViewModel> GetById(string id);

        Task<ISearchResponseModel<ArticleSearchViewModel>> GetAll(IArticleSearchModel searchModel);

        Task Create(ArticleInputModel model);

        Task Edit(ArticleEditModel model);

        Task Copy(string id);

        Task Delete(string id);

        void IncrementViews(string articleId);
    }
}
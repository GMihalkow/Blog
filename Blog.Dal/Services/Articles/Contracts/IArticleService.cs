using Blog.Dal.Models.Article;
using Blog.Dal.Models.Article.Contracts;
using Blog.Dal.Models.Common;
using System.Threading.Tasks;

namespace Blog.Dal.Services.Articles.Contracts
{
    public interface IArticleService
    {
        Task<ArticleViewModel> GetById(string id);

        Task<ISearchResponseModel<ArticleSearchViewModel>> GetAll(IArticleSearchModel searchModel);

        Task Create(ArticleInputModel model);

        Task Edit(ArticleEditModel model);

        Task Delete(string id);
    }
}
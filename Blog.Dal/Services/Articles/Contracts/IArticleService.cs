using Blog.Dal.Models.Article;
using Blog.Dal.Models.Common;
using Blog.Dal.Models.Common.Contracts;
using System.Threading.Tasks;

namespace Blog.Dal.Services.Articles.Contracts
{
    public interface IArticleService
    {
        Task<ArticleViewModel> GetById(string id);

        Task<ISearchResponseModel<ArticleSearchViewModel>> GetAll(IBaseSearchModel searchModel);

        Task Create(ArticleInputModel model);

        Task Edit(ArticleEditModel model);

        Task Delete(string id);
    }
}
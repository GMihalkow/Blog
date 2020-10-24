using Blog.Dal.Models.Category;
using Blog.Dal.Models.Common;
using Blog.Dal.Models.Common.Contracts;
using System.Threading.Tasks;

namespace Blog.Dal.Services.Categories.Contracts
{
    public interface ICategoryService
    {
        Task<CategoryViewModel> GetByName(string name);

        Task<CategoryViewModel> GetById(string id);

        Task<ISearchResponseModel<CategorySearchViewModel>> GetAll(IBaseSearchModel searchModel);

        Task Create(CategoryInputModel model);

        Task Edit(CategoryEditModel model);

        Task Delete(string id);
    }
}
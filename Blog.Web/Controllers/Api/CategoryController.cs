using Blog.Dal.Models.Category;
using Blog.Dal.Services.Categories.Contracts;
using Blog.Web.Infrastructure.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Blog.Web.Controllers.Api
{
    [Authorize(Roles = RoleConstants.Administrator)]
    public class CategoryController : BaseApiController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
            => this._categoryService = categoryService;

        [HttpGet]
        public async Task<JsonResult> GetAll([FromQuery] CategorySearchModel searchModel)
            => this.Json(await this._categoryService.GetAll(searchModel));

        [HttpGet]
        public async Task<JsonResult> GetAllForDropdown()
            => this.Json(await this._categoryService.GetAllForDropdown());
    }
}
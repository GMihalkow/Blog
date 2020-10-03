using Blog.Web.Services.Categories.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Blog.Web.Controllers.Api
{
    [ApiController]
    [Route("{api}/{controller}/{action}")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
            => this._categoryService = categoryService;

        [HttpGet]
        public async Task<JsonResult> GetAll()
            => this.Json(await this._categoryService.GetAll());
    }
}
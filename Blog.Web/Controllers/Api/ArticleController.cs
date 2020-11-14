using Blog.Dal.Models.Article;
using Blog.Dal.Services.Articles.Contracts;
using Blog.Web.Infrastructure.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Blog.Web.Controllers.Api
{
    [Authorize(Roles = RoleConstants.Administrator)]
    public class ArticleController : BaseApiController
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService) => this._articleService = articleService;

        [HttpGet]
        public async Task<JsonResult> GetAll([FromQuery] ArticleSearchModel searchModel)
        {
            var response = await this._articleService.GetAll(searchModel);

            return this.Json(response);
        }
    }
}
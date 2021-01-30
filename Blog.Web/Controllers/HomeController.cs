using Blog.Dal.Models.Index;
using Blog.Dal.Services.Articles.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Blog.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IArticleService _articleService;

        public HomeController(IArticleService articleService) => this._articleService = articleService;

        public async Task<IActionResult> Index()
        {
            var indexModel = new IndexPageViewModel
            {
                PopularArticles = await this._articleService.GetPopularArticles(3),
                LatestArticles = await this._articleService.GetLatestArticles(3),
            };

            return this.View(indexModel);
        }
    }
}
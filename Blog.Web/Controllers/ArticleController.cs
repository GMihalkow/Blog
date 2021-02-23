using Blog.Dal.Infrastructure.Constants;
using Blog.Dal.Models.Article;
using Blog.Dal.Services.Articles.Contracts;
using Blog.Dal.Services.Categories.Contracts;
using Blog.Web.Infrastructure.Constants;
using Blog.Web.Infrastructure.Extensions;
using Blog.Web.Infrastructure.Filters;
using Blog.Web.Models.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Blog.Web.Controllers
{
    [Authorize(Roles = RoleConstants.Administrator)]
    public class ArticleController : BaseController
    {
        private readonly IArticleService _articleService;
        private readonly ICategoryService _categoryService;

        public ArticleController(IArticleService articleService, ICategoryService categoryService)
        {
            this._articleService = articleService;
            this._categoryService = categoryService;
        }

        public IActionResult Search() => this.View();

        [ViewsFilter]
        [AllowAnonymous]
        public async Task<IActionResult> Details(string id)
        {
            var article = await this._articleService.GetById(id);
            if (article == null) return this.NotFound();

            article.ViewsCount++;

            return this.View(article);
        }

        public IActionResult Create() => this.View();

        [HttpPost]
        public async Task<IActionResult> Create(ArticleInputModel articleInputModel)
        {
            if (!this.ModelState.IsValid)
                return this.View(articleInputModel);

            var category = await this._categoryService.GetById(articleInputModel.CategoryId);

            if (category == null)
            {
                this.ModelState.AddModelError(string.Empty, "Invalid category selected.");
                return this.View(articleInputModel);
            }

            articleInputModel.CreatorId = await this.GetLoggedUserId();
            await this._articleService.Create(articleInputModel);
            this.TempData.AddSerialized(WebConstants.AlertKey, new Alert(AlertType.Success, string.Format(DalConstants.SuccessMessage, "created", "article")));

            return this.RedirectToAction(nameof(Search));
        }

        public async Task<IActionResult> Edit(string id)
        {
            var articleViewModel = await this._articleService.GetById(id);

            if (articleViewModel == null)
                return this.NotFound();

            if (articleViewModel.CreatorId != await this.GetLoggedUserId())
                return this.BadRequest();

            var articleEditModel = new ArticleEditModel
            {
                Id = articleViewModel.Id,
                Title = articleViewModel.Title,
                Content = articleViewModel.Content,
                CategoryId = articleViewModel.CategoryId,
                CoverUrl = articleViewModel.CoverUrl
            };

            return this.View(articleEditModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ArticleEditModel articleEditModel)
        {
            if (!this.ModelState.IsValid) return this.View(articleEditModel);

            var article = await this._articleService.GetById(articleEditModel.Id);

            if (article == null) return this.NotFound();

            var category = await this._categoryService.GetById(articleEditModel.CategoryId);
            
            if (category == null)
            {
                this.ModelState.AddModelError(string.Empty, "Invalid category selected.");
                return this.View(articleEditModel);
            }

            if (article.CreatorId != await this.GetLoggedUserId()) 
            {
                this.ModelState.AddModelError(string.Empty, "You cannot edit somebody else's articles.");
                return this.View(articleEditModel);
            }

            await this._articleService.Edit(articleEditModel);
            this.TempData.AddSerialized(WebConstants.AlertKey, new Alert(AlertType.Success, string.Format(DalConstants.SuccessMessage, "edited", "article")));

            return this.RedirectToAction(nameof(Search));
        }

        public async Task<IActionResult> Copy(string id)
        {
            var article = await this._articleService.GetById(id);

            if (article == null)
            {
                this.TempData.AddSerialized(WebConstants.AlertKey, new Alert(AlertType.Error, string.Format(DalConstants.NotFoundMessage, "Article")));

                return this.RedirectToAction(nameof(Search));
            }
            else if (article.CreatorId != await this.GetLoggedUserId())
            {
                return this.BadRequest();
            }

            await this._articleService.Copy(id);

            this.TempData.AddSerialized(WebConstants.AlertKey, new Alert(AlertType.Success, string.Format(DalConstants.SuccessMessage, "copied", "article")));

            return this.RedirectToAction(nameof(Search));
        }

        public async Task<IActionResult> Delete(string id)
        {
            var article = await this._articleService.GetById(id);

            if (article == null)
                return this.NotFound();
            else if (article.CreatorId != await this.GetLoggedUserId())
                return this.BadRequest();

            await this._articleService.Delete(id);
            this.TempData.AddSerialized(WebConstants.AlertKey, new Alert(AlertType.Success, string.Format(DalConstants.SuccessMessage, "deleted", "article")));

            return this.RedirectToAction(nameof(Search));
        }
    }
}
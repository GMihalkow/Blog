﻿using Blog.Dal.Models.Category;
using Blog.Dal.Services.Categories.Contracts;
using Blog.Web.Infrastructure.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Blog.Web.Controllers
{
    [Authorize(Roles = RoleConstants.Administrator)]
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
            => this._categoryService = categoryService;

        public IActionResult Search()
            => this.View();

        public IActionResult Create() => this.View();

        [HttpPost]
        public async Task<IActionResult> Create(CategoryInputModel categoryInputModel)
        {
            if (!this.ModelState.IsValid)
                return this.View(categoryInputModel);

            var category = await this._categoryService.GetByName(categoryInputModel.Name);

            if (category != null)
            {
                this.ModelState.AddModelError(string.Empty, "Category with this name already exists!");
                return this.View(categoryInputModel);
            }

            categoryInputModel.CreatorId = await this.GetLoggedUserId();
            await this._categoryService.Create(categoryInputModel);

            return this.RedirectToAction(nameof(Search));
        }

        public async Task<IActionResult> Edit(string id)
        {
            var category = await this._categoryService.GetById(id);

            if (category == null || category.CreatorId != await this.GetLoggedUserId())
                return this.NotFound();

            var categoryEditModel = new CategoryEditModel
            {
                Id = category.Id,
                Name = category.Name
            };

            return this.View(categoryEditModel);
        }
        
        [HttpPost]
        public async Task<IActionResult> Edit(CategoryEditModel categoryEditModel)
        {
            if (!this.ModelState.IsValid)
                return this.View(categoryEditModel);

            var categoryById = await this._categoryService.GetById(categoryEditModel.Id);

            if (categoryById == null || categoryById.CreatorId != await this.GetLoggedUserId())
                return this.NotFound();

            var categoryByName = await this._categoryService.GetByName(categoryEditModel.Name);

            if (categoryByName != null)
            {
                this.ModelState.AddModelError(string.Empty, "Category with this name already exists.");
                
                return this.View(categoryEditModel);
            }

            await this._categoryService.Edit(categoryEditModel);

            return this.RedirectToAction(nameof(Search));
        }

        public async Task<IActionResult> Delete(string id)
        {
            var category = await this._categoryService.GetById(id);

            if (category == null)
                return this.NotFound();

            await this._categoryService.Delete(id);

            return this.RedirectToAction(nameof(Search));
        }
    }
}
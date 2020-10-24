﻿using Blog.Data;
using Blog.Models;
using Blog.Dal.Infrastructure.Extensions;
using Blog.Dal.Models.Category;
using Blog.Dal.Models.Common;
using Blog.Dal.Models.Common.Contracts;
using Blog.Dal.Services.Categories.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Blog.Dal.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly BlogDbContext _dbContext;

        public CategoryService(BlogDbContext dbContext)
            => this._dbContext = dbContext;

        public async Task<CategoryViewModel> GetById(string id)
            => await this.Get(c => c.Id == id);

        public async Task<CategoryViewModel> GetByName(string name)
            => await this.Get(c => c.Name == name);

        public async Task<ISearchResponseModel<CategorySearchViewModel>> GetAll(IBaseSearchModel searchModel)
        {
            var filteredCategories = await this._dbContext
                .Categories
                .Where(c => searchModel.Keywords.IsNullOrEmpty() || c.Name.StartsWith(searchModel.Keywords))
                .Select(c => new CategorySearchViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    CreatedOn = c.CreatedOn,
                    CreatorName = c.CreatorId != null ? c.Creator.UserName : "None"
                })
                .ToListAsync();

            var data = filteredCategories
                .OptimizedSkip((searchModel.Page.Value - 1) * searchModel.Size.Value)
                .Take(searchModel.Size.Value);

            var pagesCount = (double)(filteredCategories.Count) / (double)searchModel.Size.Value;

            var responseModel = new SearchResponseModel<CategorySearchViewModel>(data, (int)Math.Ceiling(pagesCount));

            return responseModel;
        }

        public async Task Create(CategoryInputModel model)
        {
            var categoryEntity = new Category
            {
                Name = model.Name,
                CreatorId = model.CreatorId
            };

            await this._dbContext.AddAsync(categoryEntity);
            await this._dbContext.SaveChangesAsync();
        }

        public async Task Edit(CategoryEditModel model)
        {
            var category = await this._dbContext.Categories.FirstOrDefaultAsync(c => c.Id == model.Id);

            if (category == null)
                throw new InvalidOperationException("Category not found.");

            category.Name = model.Name;

            this._dbContext.Update(category);
            await this._dbContext.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            var category = await this._dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (category != null)
            {
                this._dbContext.Categories.Remove(category);
                await this._dbContext.SaveChangesAsync();
            }
        }

        private async Task<CategoryViewModel> Get(Expression<Func<Category, bool>> findCategoryfunc)
        {
            var categoryEntity = await this._dbContext.Categories.FirstOrDefaultAsync(findCategoryfunc);

            if (categoryEntity == null)
                return null;

            var categoryViewModel = new CategoryViewModel
            {
                Id = categoryEntity.Id,
                Name = categoryEntity.Name,
                CreatedOn = categoryEntity.CreatedOn,
                CreatorId = categoryEntity.CreatorId
            };

            return categoryViewModel;
        }
    }
}
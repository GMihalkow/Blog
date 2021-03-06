﻿using Blog.Dal.Models.Category;
using Blog.Dal.Models.Common;
using Blog.Dal.Models.Common.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Dal.Services.Categories.Contracts
{
    public interface ICategoryService
    {
        Task<CategoryViewModel> GetByName(string name);

        Task<CategoryViewModel> GetById(string id);

        Task<IEnumerable<CategoryDropdownListModel>> GetAllForDropdown();

        Task<ISearchResponseModel<CategorySearchViewModel>> GetAll(IBaseSearchModel searchModel);

        Task Create(CategoryInputModel model);

        Task Copy(string id);

        Task Edit(CategoryEditModel model);

        Task Delete(string id);
    }
}
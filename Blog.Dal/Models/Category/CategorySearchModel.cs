using Blog.Dal.Infrastructure.Constants;
using Blog.Dal.Models.Common.Contracts;

namespace Blog.Dal.Models.Category
{
    public class CategorySearchModel : IBaseSearchModel
    {
        public string Keywords { get; set; }

        public int? Size => DalConstants.PageSize;

        public int? Page { get; set; } = 0;
    }
}
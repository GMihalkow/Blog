using Blog.Dal.Infrastructure.Constants;
using Blog.Dal.Models.Common.Contracts;

namespace Blog.Dal.Models.Common
{
    public abstract class BaseSearchModel : IBaseSearchModel
    {
        public string Keywords { get; set; }

        public int? Size => DalConstants.PageSize;

        public int? Page { get; set; } = 0;
    }
}
using System.Collections.Generic;

namespace Blog.Dal.Models.Common
{
    public interface ISearchResponseModel<TData>
    {
        int LastPage { get; }

        IEnumerable<TData> Data { get; }
    }
}
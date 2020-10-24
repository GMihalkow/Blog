using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Blog.Dal.Models.Common
{
    public class SearchResponseModel<TData> : ISearchResponseModel<TData>
    {
        public SearchResponseModel(IEnumerable<TData> data, int lastPage)
        {
            this.Data = data;
            this.LastPage = lastPage;
        }

        [JsonPropertyName("last_page")]
        public int LastPage { get; private set; }

        public IEnumerable<TData> Data { get; private set; }
    }
}
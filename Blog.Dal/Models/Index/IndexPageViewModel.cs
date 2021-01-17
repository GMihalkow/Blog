using Blog.Dal.Models.Article;
using System.Collections.Generic;

namespace Blog.Dal.Models.Index
{
    public class IndexPageViewModel
    {
        public IEnumerable<ArticleIndexModel> PopularArticles { get; set; }

        public IEnumerable<ArticleIndexModel> LatestArticles { get; set; }
    }
}
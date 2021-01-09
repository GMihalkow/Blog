using Blog.Dal.Infrastructure.Constants;
using Blog.Dal.Models.Article.Contracts;

namespace Blog.Dal.Models.Article
{
    public class ArticleSearchModel : IArticleSearchModel
    {
        public string Keywords { get; set; }

        public int? Size => DalConstants.PageSize;

        public int? Page { get; set; } = 0;

        public string CategoryId { get; set; }

        public string CreatorId { get; set; }
    }
}
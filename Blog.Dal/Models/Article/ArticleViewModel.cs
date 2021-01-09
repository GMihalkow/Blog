using System;

namespace Blog.Dal.Models.Article
{
    public class ArticleViewModel
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public int ViewsCount { get; set; }

        public string CoverUrl { get; set; }

        public string Content { get; set; }

        public string CategoryId { get; set; }

        public string CreatorId { get; set; }

        public string CreatorName { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
using System;

namespace Blog.Dal.Models.Article
{
    public class ArticleSearchViewModel
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string CreatorId { get; set; }

        public string CreatorName { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
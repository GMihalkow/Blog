using System;

namespace Blog.Models
{
    public class Article : BaseEntity<string>
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public int ViewsCount { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatorId { get; set; }

        public User User { get; set; }

        public string CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
using System;

namespace Blog.Models
{
    public class FaqQuestion : BaseEntity<string>
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public User Creator { get; set; }

        public string CreatorId { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
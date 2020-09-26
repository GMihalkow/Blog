using System;
using System.Collections.Generic;

namespace Blog.Models
{
    public class Category : BaseEntity<string>
    {
        public string Name { get; set; }

        public ICollection<Article> Articles { get; set; }

        public string CreatorId { get; set; }

        public User Creator { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
using System;

namespace Blog.Dal.Models.Category
{
    public class CategoryViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatorId { get; set; }
    }
}
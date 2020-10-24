using System;

namespace Blog.Dal.Models.Category
{
    public class CategorySearchViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatorName { get; set; }
    }
}
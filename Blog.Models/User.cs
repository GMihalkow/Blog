using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Blog.Models
{
    public class User : IdentityUser
    {
        public ICollection<Category> Categories { get; set; }

        public ICollection<Article> Articles { get; set; }

        public ICollection<FaqQuestion> Faqs { get; set; }
    }
}
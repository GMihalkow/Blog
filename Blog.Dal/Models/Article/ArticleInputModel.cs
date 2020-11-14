using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Blog.Dal.Models.Article
{
    public class ArticleInputModel
    {
        [Required]
        [MaxLength(250)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        [DisplayName("Category")]
        public string CategoryId { get; set; }

        public string CreatorId { get; set; }
    }
}
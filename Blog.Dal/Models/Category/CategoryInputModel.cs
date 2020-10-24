using System.ComponentModel.DataAnnotations;

namespace Blog.Dal.Models.Category
{
    public class CategoryInputModel
    {
        [Required]
        [StringLength(250, MinimumLength = 6)]
        public string Name { get; set; }

        public string CreatorId { get; set; }
    }
}
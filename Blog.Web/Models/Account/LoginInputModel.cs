using System.ComponentModel.DataAnnotations;

namespace Blog.Web.Models.Account
{
    public class LoginInputModel
    {
        [Required]
        [MinLength(6)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
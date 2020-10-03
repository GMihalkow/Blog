using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
            => this.View();
    }
}
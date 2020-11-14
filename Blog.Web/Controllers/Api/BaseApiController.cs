using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers.Api
{
    [ApiController]
    [Route("{api}/{controller}/{action}")]
    public class BaseApiController : Controller
    {
    }
}
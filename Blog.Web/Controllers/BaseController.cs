using Blog.Web.Services.Users.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Blog.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        protected async Task<string> GetLoggedUserId()
        {
            if (!this.User.Identity.IsAuthenticated)
                throw new InvalidOperationException("User is not currently logged in.");

            var userService = this.HttpContext.RequestServices.GetService(typeof(IUserService)) as IUserService;

            return await userService.GetUserIdByName(this.User.Identity.Name);
        }
    }
}
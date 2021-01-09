using Blog.Web.Infrastructure.Constants;
using Blog.Web.Services.Users.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Blog.Web.Controllers.Api
{
    public class UserController : BaseApiController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService) => this._userService = userService;

        [Authorize(Roles = RoleConstants.Administrator)]
        public async Task<JsonResult> GetAllForDropdown() => this.Json(await this._userService.GetAllForDropdown());
    }
}
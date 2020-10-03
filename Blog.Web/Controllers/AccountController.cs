using Blog.Web.Models.Account;
using Blog.Web.Services.Accounts.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Blog.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
            => this._accountService = accountService;

        public IActionResult Login()
        {
            if (this.User.Identity.IsAuthenticated)
                return this.Redirect("/");

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginInputModel model)
        {
            if (this.User.Identity.IsAuthenticated)
                return this.Redirect("/");

            if (this.ModelState.IsValid)
            {
                var loginSucceeded = await this._accountService.Login(model);

                if (!loginSucceeded)
                {
                    this.ModelState.AddModelError(string.Empty, "Invalid username/password combination.");

                    return this.View(model);
                }

                return this.Redirect("/");
            }

            return this.View(model);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await this._accountService.Logout();

            return this.Redirect("/");
        }
    }
}
using Blog.Models;
using Blog.Web.Models.Account;
using Blog.Web.Services.Accounts.Contracts;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Blog.Web.Services.Accounts
{
    public class AccountService : IAccountService
    {
        private readonly SignInManager<User> _signInManager;

        public AccountService(SignInManager<User> signInManager)
            => this._signInManager = signInManager;

        public async Task<bool> Login(LoginInputModel loginInputModel)
        {
            var user = await this._signInManager.UserManager.FindByNameAsync(loginInputModel.Username);

            if (user != null)
            {
                var loginSucceeded = await this._signInManager.PasswordSignInAsync(user.UserName, loginInputModel.Password, false, false);

                return loginSucceeded.Succeeded;
            }

            return false;
        }

        public async Task Logout()
            => await this._signInManager.SignOutAsync();
    }
}
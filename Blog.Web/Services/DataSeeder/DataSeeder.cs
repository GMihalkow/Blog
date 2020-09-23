using Blog.Models;
using Blog.Web.Infrastructure.Constants;
using Blog.Web.Services.DataSeeder.Contracts;
using Microsoft.AspNetCore.Identity;

namespace Blog.Web.Services.DataSeeder
{
    public class DataSeeder : IDataSeeder
    {
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DataSeeder(SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
        {
            this._signInManager = signInManager;
            this._roleManager = roleManager;
        }

        public void SeedData()
        {
            this.SeedRoles();
            this.SeedUsers();
        }

        private void SeedRoles()
        {
            var administratorRole = new IdentityRole(RoleConstants.Administrator);
            var administratorRoleExists = this._roleManager.RoleExistsAsync(RoleConstants.Administrator).GetAwaiter().GetResult();

            if (!administratorRoleExists)
                this._roleManager.CreateAsync(administratorRole).GetAwaiter().GetResult();

            var userRole = new IdentityRole(RoleConstants.User);
            var userRoleExists = this._roleManager.RoleExistsAsync(RoleConstants.User).GetAwaiter().GetResult();

            if (!userRoleExists)
               this._roleManager.CreateAsync(userRole).GetAwaiter().GetResult();
        }

        private void SeedUsers()
        {
            var user = new User
            {
                UserName = "Tester",
                Email = "tester@gbg.bg"
            };

            var userExists = this._signInManager.UserManager.FindByNameAsync(user.UserName).GetAwaiter().GetResult() != null;

            if (!userExists)
            {
                this._signInManager.UserManager.CreateAsync(user, "testaA").GetAwaiter().GetResult();
                this._signInManager.UserManager.AddToRoleAsync(user, RoleConstants.Administrator).GetAwaiter().GetResult();
            }
        }
    }
}
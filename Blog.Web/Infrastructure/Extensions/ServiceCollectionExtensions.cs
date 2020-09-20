using Blog.Data;
using Blog.Models;
using Blog.Web.Services.Accounts;
using Blog.Web.Services.Accounts.Contracts;
using Blog.Web.Services.DataSeeder;
using Blog.Web.Services.DataSeeder.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Web.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
            => serviceCollection
                .AddTransient<IAccountService, AccountService>()
                .AddTransient<IDataSeeder, DataSeeder>();

        public static IServiceCollection AddDb(this IServiceCollection serviceCollection, IConfiguration configuration) =>
            serviceCollection
                .AddDbContext<BlogDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddIdentity<User, IdentityRole>(opts =>
            {
                opts.User.RequireUniqueEmail = true;

                opts.Password.RequiredLength = 6;
                opts.Password.RequireDigit = false;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
            })
            .AddEntityFrameworkStores<BlogDbContext>();

            return serviceCollection;
        }

        public static IServiceCollection AddMvcConfiguration(this IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddControllersWithViews()
                .AddRazorRuntimeCompilation();

            return serviceCollection;
        }
    }
}

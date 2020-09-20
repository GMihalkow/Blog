using Blog.Web.Services.DataSeeder.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Blog.Web.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseWebConfiguration(this IApplicationBuilder builder, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                builder.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                builder.UseHsts();
            }

            builder
               .UseHttpsRedirection()
               .UseStaticFiles()
               .UseRouting()
               .UseAuthentication()
               .UseAuthorization()
               .UseEndpoints(endpoints =>
               {
                   endpoints.MapControllerRoute(
                       name: "default",
                       pattern: "{controller=Home}/{action=Index}/{id?}");
               });

            return builder;
        }

        public static IApplicationBuilder SeedDb(this IApplicationBuilder builder)
        {
            using var serviceScope = builder.ApplicationServices.CreateScope();
            var serviceProvider = serviceScope.ServiceProvider;

            var seeder = serviceProvider.GetService<IDataSeeder>();
            seeder.SeedData();

            return builder;
        }
    }
}
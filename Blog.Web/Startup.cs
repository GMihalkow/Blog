using Blog.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
            => this.Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
            => services
                .AddDb(this.Configuration)
                .AddIdentityConfiguration()
                .AddMvcConfiguration()
                .AddServices();

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            => app
                .UseWebConfiguration(env)
                .SeedDb();
    }
}

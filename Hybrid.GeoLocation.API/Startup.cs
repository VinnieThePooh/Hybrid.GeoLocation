using Hybrid.GeoLocation.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Hybrid.GeoLocation
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<GeoContext>(options => {                
                var conString = Configuration.GetConnectionString("DefaultPostgresConnection");
                var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
                options.UseNpgsql(conString, builder => builder.MigrationsAssembly(assemblyName));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }            
            app.UseMvc(routes => {

                routes.MapRoute("home", "{controller}/{action}", new { controller = "Home", Action = "Index" });
            });           

            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetService<GeoContext>();
                context.Database.Migrate();               

            }
                
        }
    }
}

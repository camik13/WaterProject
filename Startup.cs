using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaterProject.Models;

// NOTES \\
/*
 * To change in this file: 
 *    1. Add services.AddControllersWithViews(); to the ConfigureServices 
 *         This helps the program recognize the MVC format
 *    2. Add app.UseStaticFiles(); before the app.UseRouting(); method
 *         This tells the program to use/recognize the wwwroot folder
 *    3. Delete everything in the UseEndpoints method and add endpoints.UseDefaultMapControllerRoute();
 *         This tells the program to go to the default controller route
 */

namespace WaterProject
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IConfiguration Configuration { get; set; }

        public Startup (IConfiguration temp)
        {
            Configuration = temp;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<WaterProjectContext>(options =>
            {
                options.UseSqlite(Configuration["ConnectionStrings:WaterDBConnection"]);
            });

            services.AddScoped<IWaterProjectRepository, EFWaterProjectRepository>(); // each HTTP request gets its own repository context
            services.AddScoped<IDonationRepository, EFDonationRespository>();

            services.AddRazorPages();

            services.AddDistributedMemoryCache();
            services.AddSession();

            services.AddScoped<Basket>(x => SessionBasket.GetBasket(x));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddServerSideBlazor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            
            // corresponds to wwwroot folder
            app.UseStaticFiles();

            app.UseSession();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                // Endpoints are executed in ORDER

                endpoints.MapControllerRoute("typepage", "{projectType}/P{pageNum}", new { Controller = "Home", action = "Index" });

                endpoints.MapControllerRoute(
                    name: "Paging",
                    pattern: "P{pageNum}", // P and then the page number will be displayed in the URL
                    defaults: new { Controller = "Home", action = "Index", pageNum=1 }
                );

                endpoints.MapControllerRoute("type", "{projectType}", new { Controller = "Home", action = "Index", pageNum = 1 });

                endpoints.MapDefaultControllerRoute(); //will map to the index file automatically

                endpoints.MapRazorPages();

                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/admin/{*catchall}", "/Admin/Index");
                
            });
        }
    }
}

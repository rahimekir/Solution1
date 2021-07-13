using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.EntityFramework.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductManager>();
            services.AddScoped<ICategoryService, CategoryManager>();

            services.AddScoped<ICategoryDal, EfCategoryDal>();
            services.AddScoped<IProductDal, EfProductDal>();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStaticFiles(); //wwwroot
            if (env.IsDevelopment())
            {
                SeedDatabase.Seed();
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                   name: "adminproductlist",
                   pattern: "admin/products",
                   defaults: new { controller = "Admin", action = "ProductList" }
                 );

                endpoints.MapControllerRoute(
                  name: "adminproductlist",
                  pattern: "admin/products/{id?}",
                  defaults: new { controller = "Admin", action ="EditProduct" }
          );
                // localhost/search    
                endpoints.MapControllerRoute(
                    name: "search",
                    pattern: "search",
                    defaults: new { controller = "Shop", action = "search" }
                );

                endpoints.MapControllerRoute(
                    name: "products",
                    pattern: "products/{category?}",
                    defaults: new { controller = "Shop", action = "list" }
                );
                endpoints.MapControllerRoute(
                 name: "productdetails",
                 pattern: "{url}",
                 defaults: new { controller = "Shop", action = "details" }
             );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }
}

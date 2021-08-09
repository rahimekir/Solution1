using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.EntityFramework.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Web.Identity;

namespace Web
{
    public class Startup
    {
        

        public void ConfigureServices(IServiceCollection services) 
        { 
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB; Database=EtProjeDb1;Integrated Security=true"));
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                //password
                options.Password.RequireDigit = true; //sayısal değer
                options.Password.RequireLowercase = true; //küçük harf 
                options.Password.RequireUppercase = true; //büyük harf
                options.Password.RequiredLength = 6; //büyük harf
                options.Password.RequireNonAlphanumeric = true; //alfa-numeric karakter

                // Lockout                
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.AllowedForNewUsers = true;

                // options.User.AllowedUserNameCharacters = "";
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            });


            services.ConfigureApplicationCookie(options => {
                options.LoginPath = "/account/login";
                options.LogoutPath = "/account/logout";
                options.AccessDeniedPath = "/account/accessdenied";
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.Cookie = new CookieBuilder
                {
                    HttpOnly = true,
                    Name = ".ShopApp.Security.Cookie",
                    SameSite = SameSiteMode.Strict
                };
            });


            services.AddScoped<ICategoryDal, EfCategoryDal>();
            services.AddScoped<IProductDal, EfProductDal>();
            services.AddScoped<ICartDal, EfCartDal>();

            services.AddScoped<IProductService, ProductManager>();
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<ICartService, CartManager>();


            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,IConfiguration configuration,UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
              Path.Combine(Directory.GetCurrentDirectory(), "node_modules")),
                RequestPath = "/modules"
            });//wwwroot
            if (env.IsDevelopment())
            {
                SeedDatabase.Seed();
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();



            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
              name: "cart",
              pattern: "cart",
              defaults: new { controller = "Cart", action = "Index" }
          );
                endpoints.MapControllerRoute(
                 name: "adminuseredit",
                 pattern: "admin/user/{id?}",
                 defaults: new { controller = "Admin", action = "UserEdit" }
             );

                endpoints.MapControllerRoute(
                   name: "adminusers",
                   pattern: "admin/user/list",
                   defaults: new { controller = "Admin", action = "UserList" }
               );

                endpoints.MapControllerRoute(
                 name: "adminroles",
                 pattern: "admin/role/list",
                 defaults: new { controller = "Admin", action = "RoleList" }
               );
                endpoints.MapControllerRoute(
                 name: "adminrolecreate",
                 pattern: "admin/role/create",
                 defaults: new { controller = "Admin", action = "RoleCreate" }
               );
                endpoints.MapControllerRoute(
                  name: "adminproducts",
                  pattern: "admin/products",
                  defaults: new { controller = "Admin", action = "ProductList" }
                );

                endpoints.MapControllerRoute(
               name: "adminproductcreate",
               pattern: "admin/products/create",
               defaults: new { controller = "Admin", action = "ProductCreate" }
             );
                endpoints.MapControllerRoute(
              name: "adminproductlist",
              pattern: "admin/products/{id?}",
              defaults: new { controller = "Admin", action = "EditProduct" }
      );

                endpoints.MapControllerRoute(
                   name: "admincategories",
                   pattern: "admin/categories",
                   defaults: new { controller = "Admin", action = "CategoryList" }
                 );
                endpoints.MapControllerRoute(
                  name: "admincategorycreate",
                  pattern: "admin/categories/create",
                  defaults: new { controller = "Admin", action = "CreateCategory" }
                );
                endpoints.MapControllerRoute(
              name: "admincategorylist",
              pattern: "admin/categories/{id?}",
              defaults: new { controller = "Admin", action = "EditCategory" }
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

            SeedIdentity.Seed(userManager,roleManager,configuration).Wait();

        }
    }
}

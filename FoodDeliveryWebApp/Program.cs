using FoodDeliveryWebApp.Areas.Identity.Data;
using FoodDeliveryWebApp.Contracts;
using FoodDeliveryWebApp.Contracts.Charts;
using FoodDeliveryWebApp.Data;
using FoodDeliveryWebApp.Hubs;
using FoodDeliveryWebApp.MiddlewareExtensions;
using FoodDeliveryWebApp.Models;
using FoodDeliveryWebApp.Models;
using FoodDeliveryWebApp.RazorRenderer;
using FoodDeliveryWebApp.Repositories;
using FoodDeliveryWebApp.Repositories.Charts;
using FoodDeliveryWebApp.SubscribeTableDependencies;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Stripe;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace FoodDeliveryWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("FoodDeliveryWebAppContextConnection") ?? throw new InvalidOperationException("Connection string 'FoodDeliveryWebAppContextConnection' not found.");

            #region Services
            builder.Services.AddDbContext<FoodDeliveryWebAppContext>(options => options.UseSqlServer(connectionString));
            builder.Services.AddSignalR(o =>
            {
                o.EnableDetailedErrors = true;
            })
                .AddJsonProtocol(c =>
            {
                c.PayloadSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            });
            builder.Services.AddScoped<IRazorPartialToStringRenderer, RazorPartialToStringRenderer>();
            builder.Services.AddSingleton<SellerOrdersIndexHub>();
            builder.Services.AddSingleton<ISubscribeTableDependency, SubscribeOrderTableDependency>();
            builder.Services.Configure<RazorViewEngineOptions>(o =>
            {
                o.ViewLocationExpanders.Add(new SubAreaViewLocationExpander());
            });
            builder.Services.AddScoped<ISellerDashboardRepo, SellerDashboardRepo>();

            #region Authentication Services

            builder.Services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<FoodDeliveryWebAppContext>().AddDefaultTokenProviders().AddDefaultUI();

            builder.Services.Configure<DataProtectionTokenProviderOptions>(opts => opts.TokenLifespan = TimeSpan.FromHours(10));

            builder.Services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            });

            builder.Services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);

                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

            builder.Services.AddAuthentication()
                .AddGoogle(opt =>
                {
                    IConfigurationSection GoogleAuthSection = builder.Configuration.GetSection("Authentication:Google");
                    opt.ClientId = GoogleAuthSection["GoogleId"];
                    opt.ClientSecret = GoogleAuthSection["GoogleSecret"];
                })
                .AddFacebook(opt =>
                {
                    IConfigurationSection FacebookAuthSection = builder.Configuration.GetSection("Authentication:Facebook");
                    opt.ClientId = FacebookAuthSection["FacebookId"];
                    opt.ClientSecret = FacebookAuthSection["FacebookSecret"];
                });

            builder.Services.AddAuthorization();
            #endregion

            #region Repository Services
            builder.Services.AddScoped<ICustomerRestaurantsRepo, CustomerRestaurantsRepo>();
            builder.Services.AddScoped<ISellerRepo, SellerRepo>();
            builder.Services.AddScoped<IPromoCodeRepo, PromoCodeRepo>();
            builder.Services.AddScoped<IModelRepo<Category>, CategoryRepo>();
            builder.Services.AddScoped<IModelRepo<Models.Review>, ReviewRepo>();
            builder.Services.AddScoped<IModelRepo<Order>, OrderRepo>();
            builder.Services.AddScoped<ModelRepo<Models.Product>, ProductRepo>();
            builder.Services.AddScoped<ICustomerOrderRepo, CustomerOrderRepo>();
            #endregion

            builder.Services.AddRazorPages();

            builder.Services.AddControllersWithViews();
            #endregion

            var app = builder.Build();

            StripeConfiguration.ApiKey = builder.Configuration["Stripe:Secret_key"];


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            {
                // DON't CHANGE THEIR ORDER
                app.UseAuthentication();
                app.UseAuthorization();
            }

            app.MapHub<SellerOrdersIndexHub>("/SellerOrdersIndexHub");

            app.MapRazorPages();

            app.MapRazorPages();



            app.MapControllerRoute(
                name: "default",
                pattern: "{area=Customer}/{controller=Restaurants}/{action=Index}/{id?}"
            );

            app.MapControllerRoute(
                name: "defaultWithArea",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
            );

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.UseSqlTableDependency<ISubscribeTableDependency>(connectionString);


            #region Seeding Roles and Create Admin User
            // when app run create the roles [ADMIN, SELLER, CUSTOMER] and make user called "admin@gmail.com", password "1234Admin."
            // and assign this admin with the role ADMIN
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    //  Initialize Roles and Users. This one will create a user with an Admin-role aswell as a separate User-role. See UserRoleInitializer.cs in Areas/Identity/Data
                       UserRoleInitializer.InitializeAsync(services).Wait();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occured while attempting to seed the database");
                }
            }
            #endregion

            app.Run();
        }
    }
}
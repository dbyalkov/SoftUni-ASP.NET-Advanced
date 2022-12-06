using HouseRentingSystem.Services.Agents;
using HouseRentingSystem.Services.Data;
using HouseRentingSystem.Services.Data.Common;
using HouseRentingSystem.Services.Data.Entities;
using HouseRentingSystem.Services.Houses;
using HouseRentingSystem.Services.Statistics;
using HouseRentingSystem.Services.Users;
using HouseRentingSystem.Web.Controllers;
using HouseRentingSystem.Web.Infrastructure;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HouseRentingSystem.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<HouseRentingDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<User>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<HouseRentingDbContext>();

            builder.Services.AddControllersWithViews(options =>
            {
                options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
            });

            builder.Services.AddTransient<IRepository, Repository>();
            builder.Services.AddTransient<IHouseService, HouseService>();
            builder.Services.AddTransient<IAgentService, AgentService>();
            builder.Services.AddTransient<IStatisticsService, StatisticsService>();
            builder.Services.AddTransient<IUserService, UserService>();

            builder.Services.AddAutoMapper(
                typeof(IHouseService).Assembly,
                typeof(HomeController).Assembly);

            builder.Services.AddResponseCaching();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseStatusCodePagesWithRedirects("/Home/Error?statusCode={0}");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "default",
                  pattern: "{controller=Home}/{action=Index}/{id?}"
                );

                endpoints.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );

                endpoints.MapControllerRoute(
                  name: "houseDetails",
                  pattern: "House/Details/{id}/{information}"
                );

                endpoints.MapRazorPages();
            });

            app.SeedAdmin();

            app.UseResponseCaching();

            app.Run();
        }
    }
}
using Microsoft.EntityFrameworkCore;
using PasswordManager.Contexts;
using PasswordManager.Models;

namespace PasswordManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            string connection = builder.Configuration.GetConnectionString("DefaultConnection");

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<ClientContext>(options => options.UseSqlServer(connection));
            builder.Services.AddAuthentication("PasswordManagerAuth")
                .AddCookie("PasswordManagerAuth", options =>
                {
                    options.LoginPath = "/Account/Login";
                    //options.AccessDeniedPath = "/Login/AccessDenied";
                });
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireRole(EnumClientType.Admin.ToString()));
                options.AddPolicy("User", policy => policy.RequireRole(EnumClientType.Personal.ToString(), EnumClientType.Personal.ToString()));
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}

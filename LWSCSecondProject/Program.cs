using LWSCSecondProject.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using LWSCSecondProject.Services;

namespace LWSCSecondProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            //  to get connection string 

            builder.Services.AddTransient<IEmailSender,EmailSender>();  
            var  connStr= builder.Configuration.GetConnectionString("MyConn");

            builder.Services.AddDbContext<MyDbContext>(options =>
            {
                options.UseSqlServer(connStr);
            }
            );

            //builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<MyDbContext>();

           // builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<MyDbContext>();


            // Register Aspnet identity
            builder.Services.AddIdentity<AppUser, AppRole>(options => {
            
                options.Password.RequireNonAlphanumeric = false;
              
            
            }).AddEntityFrameworkStores<MyDbContext>().AddDefaultTokenProviders();



            builder.Services.ConfigureApplicationCookie(options => {

                options.LoginPath = "/Identity/Account/Login";
            });
        
            // Add services to the container.
            builder.Services.AddMvc();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
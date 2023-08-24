using LWSCSecondProject.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using LWSCSecondProject.Services;
using LWSCSecondProject.SeadingData;
using LWSCSecondProject.Hubs;

namespace LWSCSecondProject
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);



            //  enable cors 

            var policy = "MY_CORS_POLICY";


            //builder.Services();

            builder.Services.AddCors(options => {

                options.AddPolicy(policy, p =>
                {
                    var sites = builder.Configuration.GetSection("AllowedSites").Get<string[]>();
                    p.WithOrigins(sites).AllowAnyHeader()
                            .AllowAnyMethod(); ;
                });

                //options.AddPolicy(policy, builder => {

                //    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                //});
            
            });


            //  to get connection string 

            builder.Services.AddTransient<IEmailSender,EmailSender>();  
            var  connStr= builder.Configuration.GetConnectionString("MyConn");

            builder.Services.AddDbContext<MyDbContext>(options =>
            {
                options.UseSqlServer(connStr);
            }
            );


            builder.Services.AddTransient<INotificationManager, NotificationManager>();  

            builder.Services.AddSignalR(options => { });


            builder.Services.AddSwaggerGen(options => { });

            //builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<MyDbContext>();

           // builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<MyDbContext>();


            // Register Aspnet identity(UserManager, SignInManager,...etc)
            builder.Services.AddIdentity<AppUser, AppRole>(options => {
            
                options.Password.RequireNonAlphanumeric = false;
              
            
            }).AddEntityFrameworkStores<MyDbContext>().AddDefaultTokenProviders();



            builder.Services.ConfigureApplicationCookie(options => {

                options.LoginPath = "/Identity/Account/Login";
                //options.ExpireTimeSpan= TimeSpan.FromMinutes(10);
                //options.SlidingExpiration = false;
            });


            builder.Services.AddAutoMapper(typeof(AutoMapperConfig));
            // Add services to the container.
            builder.Services.AddMvc();




           

            var app = builder.Build();
            app.UseCors(policy);
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();

             
            }


          
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseRouting();


            using(var  scope =app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetService<RoleManager<AppRole>>();
                var userManager = scope.ServiceProvider.GetService<UserManager<AppUser>>();


                await SeadRoles.InitializeRoles(roleManager);
                await SeadUsers.AddDefaultUser(userManager);


            }


            app.UseAuthorization();

            app.MapHub<ChatHub>("/chat");  //  url  for client side 
            app.MapHub<NotificationHub>("/notification");  //  url  for client side 

            app.MapRazorPages();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
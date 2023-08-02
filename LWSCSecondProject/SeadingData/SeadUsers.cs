using LWSCSecondProject.Entities;
using Microsoft.AspNetCore.Identity;

namespace LWSCSecondProject.SeadingData
{
    public class SeadUsers
    {

        public static async Task AddDefaultUser(UserManager<AppUser> userManager)
        {

            if(!userManager.Users.Any())
            {

                var user = new AppUser() { Email = "admin@myapp.ps", UserName = "admin@myapp.ps", FullName = "System  Admin" };
              await  userManager.CreateAsync( user, "123@Qwe");


                await userManager.AddToRoleAsync(user, "Admins");
            }

        }
    }
}

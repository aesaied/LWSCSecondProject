using LWSCSecondProject.Entities;
using Microsoft.AspNetCore.Identity;

namespace LWSCSecondProject.SeadingData
{
    public class SeadRoles
    {

        public static async Task InitializeRoles(RoleManager<AppRole> roleManager)
        {

            string adminRole = "Admins";
            string userRole = "Users";


            if (!await roleManager.RoleExistsAsync(adminRole)) {

               await roleManager.CreateAsync(new AppRole() { Name = adminRole });
            }

            if (!await roleManager.RoleExistsAsync(userRole))
            {

                await roleManager.CreateAsync(new AppRole() { Name = userRole });
            }
        }
    }
}

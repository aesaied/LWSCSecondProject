using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LWSCSecondProject.Entities
{
    public class AppUser:IdentityUser
    {

        //  Add additional  properties

        public  string? FullName { get; set; }
    }
}

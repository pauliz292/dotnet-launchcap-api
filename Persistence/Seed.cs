using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context,
            UserManager<AppUser> userManager, RoleManager<Role> roleManager)
        {
            if (!roleManager.Roles.Any())
            {
                string admin = "Admin";
                string user = "User";

                var roles = new List<Role>
                {
                    new Role {
                        Id=1,
                        Name=admin,
                        NormalizedName=admin.ToUpper()
                    },
                    new Role {
                        Id=2,
                        Name=user,
                        NormalizedName=user.ToUpper()
                    }
                };

                foreach (var role in roles)
                {
                    await roleManager.CreateAsync(role);
                }
            }

            if (!userManager.Users.Any())
            {
                var admin = new AppUser
                {
                    Id = 1,
                    UserName = "superadmin",
                    Email = "admin@admin.com",
                    EmailConfirmed = true,
                    IsActive = true

                };

                await userManager.CreateAsync(admin, "Pa$$w0rd");
                await userManager.AddToRoleAsync(admin, "Admin");
            }
        }
    }
}
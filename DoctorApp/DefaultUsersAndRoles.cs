using DoctorApp.Modals;
using Microsoft.AspNetCore.Identity;

namespace DoctorApp
{
    public static class DefaultUsersAndRoles
    {
        public static void SeedData(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUser(userManager);
        }

        private static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            try
            {
                if (!roleManager.RoleExistsAsync("Admin").Result)
                {
                    IdentityRole role = new IdentityRole()
                    {
                        Name = "Admin",
                    };
                    var result = roleManager.CreateAsync(role).Result;
                }
                if (!roleManager.RoleExistsAsync("Doctor").Result)
                {
                    IdentityRole role = new IdentityRole()
                    {
                        Name = "Doctor",
                    };
                    var result = roleManager.CreateAsync(role).Result;
                }
                if (!roleManager.RoleExistsAsync("Patient").Result)
                {
                    IdentityRole role = new IdentityRole()
                    {
                        Name = "Patient",
                    };
                    var result = roleManager.CreateAsync(role).Result;
                }
                if (!roleManager.RoleExistsAsync("Editor").Result)
                {
                    IdentityRole role = new IdentityRole()
                    {
                        Name = "Editor",
                    };
                    var result = roleManager.CreateAsync(role).Result;
                }
            }
            catch (System.Exception)
            {
            }
        }

        private static void SeedUser(UserManager<ApplicationUser> userManager)
        {
            if (userManager.FindByEmailAsync("admin@gmail.com").Result == null)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    Email = "admin@gmail.com",
                    UserName = "admin@gmail.com",
                    EmailConfirmed = true,
                };
                var result = userManager.CreateAsync(user, "Bscs@123456").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }
    }
}
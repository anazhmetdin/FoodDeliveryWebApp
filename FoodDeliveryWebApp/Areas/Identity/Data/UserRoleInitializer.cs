using FoodDeliveryWebApp.Constants;
using Microsoft.AspNetCore.Identity;

namespace FoodDeliveryWebApp.Areas.Identity.Data
{
    public static class UserRoleInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

            string[] roleNames = { Roles.ADMIN, Roles.SELLER, Roles.CUSTOMER };

            IdentityResult roleResult;

            foreach (var role in roleNames)
            {
                var roleExists = await roleManager.RoleExistsAsync(role);

                if (!roleExists)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            var email = "admin@gmail.com";
            var password = "1234Admin.";

            if (userManager.FindByEmailAsync(email).Result == null)
            {
                AppUser user = new()
                {
                    Email = email,
                    UserName = email,
                    FirstName = "Mohaned",
                    LastName = "Saudi",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };

                IdentityResult result = userManager.CreateAsync(user, password).Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, Roles.ADMIN).Wait();
                }
            }

        }
    }
}

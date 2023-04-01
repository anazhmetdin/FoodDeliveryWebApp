using FoodDeliveryWebApp.Constants;
using Microsoft.AspNetCore.Identity;

namespace FoodDeliveryWebApp.Areas.Identity.Data
{
    public static class FoodDeliveryWebAppContextSeedData
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider service)
        {
            //Seed Roles

            // 1- getting the services (API's) responsible for `Users` and `roles` manging
            var userManager = service.GetService<UserManager<AppUser>>();
            var roleManager = service.GetService<RoleManager<IdentityRole>>();

            // 2- creating and adding roles to the `roleManger` Instance
            if (roleManager is not null && userManager is not null)
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.ADMIN));
                await roleManager.CreateAsync(new IdentityRole(Roles.SELLER));
                await roleManager.CreateAsync(new IdentityRole(Roles.CUSTOMER));


                // creating admin in code to be secure. 

                var user = new AppUser
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    FirstName = "Mohaned",
                    LastName = "Saudi",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };
                var userInDb = await userManager.FindByEmailAsync(user.Email);


                // If no user is found with that email (Email = "admin@gmail.com")
                // Then we create an new account and set it as admin.

                // This part ensures that there is always an admin account.
                if (userInDb == null)
                {
                    // setting it's password
                    await userManager.CreateAsync(user, "1234Admin.");
                    // setting it's role
                    await userManager.AddToRoleAsync(user, Roles.ADMIN);
                }

                /* If the email does exist, we will assing the role in the 
                 register.cs for some reason ====> search !! 
            
                 */
            }
        }

    }
}

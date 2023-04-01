using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using FoodDeliveryWebApp.Data;

namespace FoodDeliveryWebApp.Areas.Identity.Data
{
    //public class FoodDeliveryWebAppContextSeedData
    //{
    //    public static void Initialize(IServiceProvider serviceProvider)
    //    {
    //        var context = serviceProvider.GetService<FoodDeliveryWebAppContext>();

    //        string[] roles = new string[] { "Admin", "Seller", "Customer"};

    //        foreach (string role in roles)
    //        {
    //            var roleStore = new RoleStore<IdentityRole>(context);

    //            if (!context.Roles.Any(r => r.Name == role))
    //            {
    //                roleStore.CreateAsync(new IdentityRole(role));
    //            }
    //        }


    //        var user = new AppUser
    //        {
    //            FirstName = "mohaned",
    //            LastName = "saudi",
    //            Email = "admin@gmail.com",
    //            NormalizedEmail = "ADMIN@GMAIL.COM",
    //            UserName = "Mohaned",
    //            NormalizedUserName = "MOHANED",
    //            PhoneNumber = "01011111111",
    //            EmailConfirmed = true,
    //            PhoneNumberConfirmed = true,
    //            SecurityStamp = Guid.NewGuid().ToString("D")
    //        };


    //        if (!context.Users.Any(u => u.UserName == user.UserName))
    //        {
    //            var password = new PasswordHasher<AppUser>();
    //            var hashed = password.HashPassword(user, "1234Admin.");
    //            user.PasswordHash = hashed;

    //            var userStore = new UserStore<AppUser>(context);
    //            var result = userStore.CreateAsync(user);

    //        }

    //        AssignRoles(serviceProvider, user.Email, roles);

    //        context.SaveChangesAsync();
    //    }

    //    public static async Task<IdentityResult> AssignRoles(IServiceProvider services, string email, string[] roles)
    //    {
    //        UserManager<AppUser> _userManager = services.GetService<UserManager<AppUser>>();
    //        AppUser user = await _userManager.FindByEmailAsync(email);
    //        var result = await _userManager.AddToRolesAsync(user, roles);

    //        return result;
    //    }

    //}
}

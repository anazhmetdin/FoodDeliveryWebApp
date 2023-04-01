using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using FoodDeliveryWebApp.Data;

namespace FoodDeliveryWebApp.Areas.Identity.Data
{
    //public class FoodDeliveryWebAppDbContextSeedData
    //{
    //    private FoodDeliveryWebAppContext _context;

    //    public FoodDeliveryWebAppDbContextSeedData(FoodDeliveryWebAppContext context)
    //    {
    //        _context = context;
    //    }

    //    public async void SeedAdminUser()
    //    {
    //        var user = new AppUser
    //        {
    //            UserName = "admin@gmail.com",
    //            NormalizedUserName = "email@gmail.com",
    //            Email = "admin@gmail.com",
    //            NormalizedEmail = "email@gmail.com",
    //            EmailConfirmed = true,
    //            LockoutEnabled = false,
    //            SecurityStamp = Guid.NewGuid().ToString()
    //        };

    //        var roleStore = new RoleStore<IdentityRole>(_context);

    //        if (!_context.Roles.Any(r => r.Name == "Admin"))
    //        {
    //            await roleStore.CreateAsync(new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" });
    //        }

    //        if (!_context.Users.Any(u => u.UserName == user.UserName))
    //        {
    //            var password = new PasswordHasher<AppUser>();
    //            var hashed = password.HashPassword(user, "1234Admin.");
    //            user.PasswordHash = hashed;
    //            var userStore = new UserStore<AppUser>(_context);
    //            await userStore.CreateAsync(user);
    //            await userStore.AddToRoleAsync(user, "Admin");
    //        }

    //        await _context.SaveChangesAsync();
    //    }
  //  }
}

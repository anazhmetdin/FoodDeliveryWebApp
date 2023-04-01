using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryWebApp.Areas.Identity.Data
{
    //public static class ModelBuilderExtensions
    //{

    //    public static void Seed(this ModelBuilder builder)
    //    {

    //        // Seed Roles

    //        List<IdentityRole> roles = new List<IdentityRole>() {
    //           new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" },
    //           new IdentityRole { Name = "Seller", NormalizedName = "SELLER" },
    //           new IdentityRole { Name = "Customer", NormalizedName = "CUSTOMER" }

    //        };

    //        builder.Entity<IdentityRole>().HasData(roles);

    //        // Seed Users

    //        var passwordHasher = new PasswordHasher<AppUser>();

    //     // imporant: don't forget NormalizedUserName, NormalizedEmail 
    //           var user =  new AppUser {
    //                UserName = "admin@gmail.com",
    //                NormalizedUserName = "Admin@gmail.com",
    //                Email = "admin@gmail.com",
    //                NormalizedEmail = "ADMIN@gmail.com"
    //           };


    //        builder.Entity<AppUser>().HasData(user);

    //        // Seed UserRoles

    //        List<IdentityUserRole<string>> userRoles = new List<IdentityUserRole<string>>();

    //        // Add Password For All Users

    //        //user.PasswordHash = passwordHasher.HashPassword(user, "1234Seller.");
    //        user.PasswordHash = passwordHasher.HashPassword(user, "1234Admin.");

    //        userRoles.Add(new IdentityUserRole<string>
    //        {
    //            UserId = user.Id,
    //            RoleId = roles.First(q => q.Name == "Admin").Id
    //        });

    //        builder.Entity<IdentityUserRole<string>>().HasData(userRoles);

    //    }
    //}
}

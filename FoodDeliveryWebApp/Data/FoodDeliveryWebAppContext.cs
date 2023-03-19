using FoodDeliveryWebApp.Areas.Identity.Data;
using FoodDeliveryWebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryWebApp.Data;

public class FoodDeliveryWebAppContext : IdentityDbContext<AppUser>
{
    public FoodDeliveryWebAppContext(DbContextOptions<FoodDeliveryWebAppContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Product>(b =>
        {
            b.Property(p => p.Price).HasColumnType("money");
        });

        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

    public DbSet<Product> Products { get; set; }
}

using FoodDeliveryWebApp.Areas.Identity.Data;
using FoodDeliveryWebApp.Models;
using FoodDeliveryWebApp.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Reflection.Emit;

namespace FoodDeliveryWebApp.Data;

public class FoodDeliveryWebAppContext : IdentityDbContext<AppUser>
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Seller> Sellers { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<CustomerOrderProduct> CustomerOrderProducts { get; set; }
    public DbSet<PromoCode> PromoCodes { get; set; }

    public FoodDeliveryWebAppContext(DbContextOptions<FoodDeliveryWebAppContext> options) : base(options){}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);

        builder.Entity<Seller>().HasKey(s => s.UserId);
        builder.Entity<Seller>().HasIndex(s => s.StoreName).IsUnique();
        builder.Entity<Customer>().HasKey(s => s.UserId);

        builder.Entity<Product>().HasIndex(s => s.HasSale);

        builder.Entity<Product>(b =>
        {
            b.Property(p => p.Price).HasColumnType("money");
            b.Property(p => p.Image).HasColumnType("image");
        });

        builder.Entity<Order>(b =>
        {
            b.Property(o => o.TotalPrice).HasColumnType("money");
            
            b.Property(o => o.Status)
            .HasConversion(new EnumToStringConverter<OrderStatus>());
        });

        builder.Entity<CustomerOrderProduct>(b =>
        {
            b.HasKey(cop => new { cop.ProductId, cop.OrderId, cop.CustomerId });

            b.HasOne(cop => cop.Product)
             .WithMany(o => o.CustomerOrderProducts)
             .HasForeignKey(cop => cop.ProductId)
             .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(cop => cop.Order)
             .WithMany(o => o.CustomerOrderProducts)
             .HasForeignKey(cop => cop.OrderId)
             .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(cop => cop.Customer)
              .WithMany(o => o.CustomerOrderProducts)
              .HasForeignKey(cop => cop.CustomerId)
              .OnDelete(DeleteBehavior.Restrict);
        });

    }
}

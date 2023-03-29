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
    public DbSet<OrderProduct> OrderProducts { get; set; }
    public DbSet<SellerCategories> SellerCategories { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<PromoCode> PromoCodes { get; set; }

    public FoodDeliveryWebAppContext(DbContextOptions<FoodDeliveryWebAppContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);

        builder.Entity<Seller>(b =>
        {
            b.HasIndex(s => s.StoreName).IsUnique();

            b.HasMany(s => s.SellerCategories)
           .WithOne(op => op.Seller)
           .HasForeignKey(op => op.SellerId);
        });

        builder.Entity<Category>(b =>
        {
            b.HasMany(s => s.SellerCategories)
           .WithOne(op => op.Category)
           .HasForeignKey(op => op.CategoryId);
        });

        builder.Entity<Product>(b =>
        {
            b.HasMany(p => p.OrderProducts)
            .WithOne(op => op.Product)
            .HasForeignKey(op => op.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

            b.Property(p => p.Price).HasColumnType("money");
            b.Property(p => p.Image).HasColumnType("image");
        });

        builder.Entity<Order>(b =>
        {
            b.HasMany(o => o.OrderProducts)
            .WithOne(op => op.Order)
            .HasForeignKey(op => op.OrderId);

            b.Property(o => o.TotalPrice).HasColumnType("money");

            b.Property(o => o.Status)
            .HasConversion(new EnumToStringConverter<OrderStatus>());
        });

        builder.Entity<OrderProduct>(b =>
        {
            b.HasKey(o => new { o.ProductId, o.OrderId });
        });
        
        builder.Entity<SellerCategories>(b =>
        {
            b.HasKey(o => new { o.CategoryId, o.SellerId });
        });
    
        builder.Entity<PromoCode>(b =>
        {
            b.Property(p => p.MaximumDiscount).HasColumnType("money");
        });
    }
}

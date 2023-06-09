﻿using FoodDeliveryWebApp.Areas.Identity.Data;
using FoodDeliveryWebApp.Models;
using FoodDeliveryWebApp.Models.Enums;
using FoodDeliveryWebApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Reflection.Emit;
using System.Reflection.Metadata;

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
    public DbSet<Category> Categories { get; set; }
    public DbSet<PromoCode> PromoCodes { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<TrendingSeller> TrendingSellers { get; set; }

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

            b.Property(s => s.Logo).IsRequired(false);

            b.HasMany(s => s.Reviews)
            .WithOne(op => op.Seller)
            .HasForeignKey(r => r.SellerId);

            b.HasMany(s => s.Products)
            .WithOne(op => op.Seller)
            .HasForeignKey(r => r.SellerId);

            b.HasMany(s => s.Categories)
           .WithMany(op => op.Sellers);

            b.Property(s => s.Rate)
            .HasDefaultValue(0);
        });

        builder.Entity<Customer>(b =>
        {
            b.Property(c => c.ProfilePicture).IsRequired(false);
        });

        builder.Entity<Category>(b =>
        {
            b.HasMany(s => s.Sellers)
           .WithMany(op => op.Categories);

            b.HasMany(s => s.Products)
           .WithOne(op => op.Category)
           .HasForeignKey(p => p.CategoryId)
           .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<Product>(b =>
        {
            b.HasMany(p => p.OrderProducts)
            .WithOne(op => op.Product)
            .HasForeignKey(op => op.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(p => p.Seller)
            .WithMany(op => op.Products);

            b.Property(p => p.Price).HasColumnType("money");
            b.Property(p => p.Image).HasColumnType("image").IsRequired();
        });

        builder.Entity<Order>()
            .ToTable(tb => tb.HasTrigger("SellerOrderIndex"));

        builder.Entity<Order>(b =>
        {
            b.Property(o => o.CheckOutDate).IsRequired(false);
            b.Property(o => o.DeliveryDate).IsRequired(false);
            b.Property(o => o.ReviewId).IsRequired(false);

            b.HasOne(r => r.Address)
            .WithMany()
            .HasForeignKey(r => r.AddressId)
            .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(r => r.Review)
            .WithMany()
            .HasForeignKey(r => r.ReviewId)
            .OnDelete(DeleteBehavior.Restrict);

            b.HasMany(o => o.OrderProducts)
            .WithOne(op => op.Order)
            .HasForeignKey(op => op.OrderId)
            .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(o => o.Address)
            .WithMany(o => o.Orders)
            .HasForeignKey(op => op.AddressId)
            .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(o => o.Seller)
            .WithMany(op => op.Orders)
            .HasForeignKey(op => op.SellerId)
            .OnDelete(DeleteBehavior.Restrict);

            b.Property(o => o.TotalPrice).HasColumnType("money");

            b.Property(o => o.Status)
            .HasConversion(new EnumToStringConverter<OrderStatus>());

        });

        builder.Entity<Review>(b =>
        {
            b.HasOne(r => r.Seller)
            .WithMany(op => op.Reviews)
            .HasForeignKey(r => r.SellerId)
            .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<OrderProduct>(b =>
        {
            b.HasKey(o => new { o.ProductId, o.OrderId });
            b.Property(o => o.UnitPrice).HasColumnType("money");
        });

        builder.Entity<PromoCode>(b =>
        {
            b.Property(p => p.MaximumDiscount).HasColumnType("money");

            b.HasIndex(s => s.Code).IsUnique();
        });

        builder.Entity<TrendingSeller>(b =>
        {
            b.HasKey(ts => ts.Id);

            b.HasOne(ts => ts.Seller);
        });
    }
}

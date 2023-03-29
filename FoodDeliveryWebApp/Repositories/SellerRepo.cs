using FoodDeliveryWebApp.Contracts;
using FoodDeliveryWebApp.Data;
using FoodDeliveryWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryWebApp.Repositories
{
    public class SellerRepo : ModelRepo<Seller>, ISellerRepo
    {
        public SellerRepo(FoodDeliveryWebAppContext context): base(context) { }

        public ICollection<Product> GetSellerProducts(string sellerId)
        {
            var products = Context.Products
                .Where(p => p.SellerId == sellerId)
                .ToList();

            return products;
        }

        public void CreateProduct(Product product)
        {
            Context.Products.Add(product);

            Context.SaveChanges();
        }

        public ICollection<Category> GetSellerCategories(string sellerId)
        {
            var seller = Context.Sellers.Where( s => s.Id == sellerId)
                .Include(s => s.SellerCategories)
                .ThenInclude(op => op.Category).FirstOrDefault();


            return seller.SellerCategories.Select(s => s.Category).ToList();
        }
    }
}

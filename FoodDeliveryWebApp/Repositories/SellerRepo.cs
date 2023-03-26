using FoodDeliveryWebApp.Contracts;
using FoodDeliveryWebApp.Data;
using FoodDeliveryWebApp.Models;

namespace FoodDeliveryWebApp.Repositories
{
    public class SellerRepo :ISellerRepo
    {
        private readonly FoodDeliveryWebAppContext _context;

        public SellerRepo(FoodDeliveryWebAppContext context)
        {
            _context = context;
        }

        public ICollection<Product> GetSellerProducts(string sellerId)
        {
            var products = _context.Products
                .Where(p => p.SellerId == sellerId)
                .ToList();

            return products;
        }

        public void CreateProduct(Product product)
        {
            _context.Products.Add(product);

            _context.SaveChanges();
        }
    }
}

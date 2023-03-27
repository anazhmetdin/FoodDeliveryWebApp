using FoodDeliveryWebApp.Contracts;
using FoodDeliveryWebApp.Data;
using FoodDeliveryWebApp.Models;

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
    }
}

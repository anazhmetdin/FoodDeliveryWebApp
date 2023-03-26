using FoodDeliveryWebApp.Models;

namespace FoodDeliveryWebApp.Contracts
{
    public interface ISellerRepo
    {
        public ICollection<Product> GetSellerProducts(string sellerId);
        public void CreateProduct(Product product);
    }
}

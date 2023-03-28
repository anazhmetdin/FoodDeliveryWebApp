using FoodDeliveryWebApp.Models;
using FoodDeliveryWebApp.Repositories;
using System.Runtime.InteropServices;

namespace FoodDeliveryWebApp.Contracts
{
    public interface ISellerRepo: IModelRepo<Seller>
    {
        public ICollection<Product> GetSellerProducts(string? sellerId);
        public Product? GetSellerProduct(int pid, string? sellerId);
        public void CreateProduct(Product product);
        public void Restock(int pid, string? sid, bool stock);
        public void Restock(IFormCollection pairs, string? sid, bool stock);
    }
}

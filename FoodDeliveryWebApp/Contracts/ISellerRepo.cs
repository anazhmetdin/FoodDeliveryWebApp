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
        public void Restock(int pid, string? sellerId, bool stock);
        public void Restock(IFormCollection pairs, string? sellerId, bool stock);
        public void ApplySale(IFormCollection pairs, string? sellerId);
        public void ApplySale(int id, string sellerId, int sale);
    }
}

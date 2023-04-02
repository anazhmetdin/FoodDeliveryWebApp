using FoodDeliveryWebApp.Models;
using FoodDeliveryWebApp.Models.Enums;
using System.Runtime.InteropServices;

namespace FoodDeliveryWebApp.Contracts
{
    public interface ISellerRepo: IModelRepo<Seller>
    {
        public ICollection<Category> GetSellerCategories(string sellerId);

        public ICollection<Product> GetSellerProducts(string? sellerId);
        public Product? GetSellerProduct(int pid, string? sellerId);

        public bool TryAddProduct(string? sellerId, Product product, IFormFile Image);
        public bool TryUpdateProduct(string? sellerId, Product product, IFormFile? Image);

        public void Restock(IFormCollection pairs, string? sellerId, bool stock);
        public void ApplySale(IFormCollection pairs, string? sellerId);
        public void ApplySale(int id, string sellerId, int sale);
        public List<Order> GetOrders(string? sellerId, OrderStatus orderStatus);
        public Order? GetOrder(int? id, string? sellerId);
        public bool ChangeOrderStatus(int? id, string? sellerId, OrderStatus? status);
        public ICollection<Review> GetReviews(string? sellerId);
        public List<int> GetSalesYears(string sellerId);
        public List<Order> GetSalesPerYear(string sellerId, int year);

        public void UpdateSellerCategories(string? sellerId);
    }
}

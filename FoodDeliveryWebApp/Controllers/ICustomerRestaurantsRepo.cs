using FoodDeliveryWebApp.Areas.Identity.Data;
using FoodDeliveryWebApp.Models;
using FoodDeliveryWebApp.ViewModels;

namespace FoodDeliveryWebApp.Contracts
{
    public interface ICustomerRestaurantsRepo
    {

        public ICollection<SellerViewModel> GetSellers();
        public string GetProductSellerID(int productId);
        public Order CreateOrder(string sellerId, string customerId);
        public OrderProduct CreateOrderProduct(int orderId, int prodId);
        public bool UpdateOrder(Order o);

        public ICollection<ProductViewModel> GetSellerProducts(string sellerId);

        public ICollection<SellerViewModel> GetSellersFiltered(List<Category> categories);

    }
}
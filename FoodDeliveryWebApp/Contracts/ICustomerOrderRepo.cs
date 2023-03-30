using FoodDeliveryWebApp.ViewModels;

namespace FoodDeliveryWebApp.Contracts
{
    public interface ICustomerOrderRepo
    {
        public ICollection<OrderViewModel> GetOrders(string id);
        
        public OrderViewModel GetOrder(int id);
    }
}

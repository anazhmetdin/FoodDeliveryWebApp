using FoodDeliveryWebApp.ViewModels;

namespace FoodDeliveryWebApp.Contracts
{
    public interface ICustomerOrderRepo
    {
        public ICollection<OrderViewModel> GetOrders(string id);

        public OrderViewModel GetOrder(int id);
        public bool DeleteOrderById(int orderId);

        public bool AddReview(ReviewViewModel review, int orderId);

    }
}

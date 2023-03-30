using FoodDeliveryWebApp.Models;
using System.ComponentModel;

namespace FoodDeliveryWebApp.ViewModels
{
    public class SellerOrdersViewModel
    {
        [DisplayName("Posted")]
        public ICollection<Order> PostedOrders { get; set; } = new List<Order>();
        [DisplayName("In Progress")]
        public ICollection<Order> InProgressOrders { get; set; } = new List<Order>();
        [DisplayName("Rejected")]
        public ICollection<Order> RejectedOrders { get; set; } = new List<Order>();
        [DisplayName("Delivered")]
        public ICollection<Order> DeliveredOrders { get; set; } = new List<Order>();
    }
}

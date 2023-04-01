using FoodDeliveryWebApp.Models;
using System.ComponentModel;

namespace FoodDeliveryWebApp.ViewModels
{
    public class SellerOrderArchiveViewModel
    {
        [DisplayName("Delivered")]
        public ICollection<Order> DeliveredOrders { get; set; } = new List<Order>();
        [DisplayName("Rejected")]
        public ICollection<Order> RejectedOrders { get; set; } = new List<Order>();
    }
}

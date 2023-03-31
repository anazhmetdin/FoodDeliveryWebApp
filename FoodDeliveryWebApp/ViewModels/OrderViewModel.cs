using FoodDeliveryWebApp.Models;
using FoodDeliveryWebApp.Models.Enums;

namespace FoodDeliveryWebApp.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        public decimal TotalPrice { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public DateTime? CheckOutDate { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public Review Review { get; set; } = new();

        public Seller Seller { get; set; } = new();

        public PromoCode? PromoCode { get; set; }

        public ICollection<ProductViewModel> Products { get; set; } = new List<ProductViewModel>();
    }
}

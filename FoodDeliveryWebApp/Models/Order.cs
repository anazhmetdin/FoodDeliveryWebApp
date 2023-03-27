using FoodDeliveryWebApp.Areas.Identity.Data;
using FoodDeliveryWebApp.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryWebApp.Models
{
    public class Order:BaseModel
    {
        public decimal TotalPrice { get; set;}
        public DateTime DeliveryDate { get; set;}
        public DateTime CheckOutDate { get; set;}
        [ForeignKey("Customer")]
        public string CustomerId { get; set; } = string.Empty;

        public virtual Customer Customer { get; set; } = new();

        public virtual Review Review { get; set; } = new();

        public virtual ICollection<CustomerOrderProduct> CustomerOrderProducts { get; set; } = new List<CustomerOrderProduct>();
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public int? PromoCodeId { get; set; }
        public virtual PromoCode? PromoCode { get; set; }
    }
}

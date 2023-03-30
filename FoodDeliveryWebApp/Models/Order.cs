using FoodDeliveryWebApp.Areas.Identity.Data;
using FoodDeliveryWebApp.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryWebApp.Models
{
    public class Order : BaseModel
    {
        public decimal TotalPrice { get; set; } = decimal.Zero;

        public DateTime? DeliveryDate { get; set; }

        public DateTime? CheckOutDate { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        [ForeignKey("Customer")]
        public string CustomerId { get; set; } = string.Empty;

        public virtual Customer Customer { get; set; } = new();

        [ForeignKey("Review")]
        public int? ReviewId { get; set; }

        public virtual Review? Review { get; set; }

        [ForeignKey("seller")]
        public string SellerId { get; set; } = string.Empty;

        public virtual Seller? Seller { get; set; }

        [ForeignKey("PromoCode")]
        public int? PromoCodeId { get; set; }

        public virtual PromoCode? PromoCode { get; set; }

        public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
    }
}

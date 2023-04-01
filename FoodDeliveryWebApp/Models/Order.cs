using FoodDeliveryWebApp.Areas.Identity.Data;
using FoodDeliveryWebApp.Models.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryWebApp.Models
{
    public class Order:BaseModel
    {
        [DisplayName("Total")]
        [Range(0, (double)decimal.MaxValue)]
        public decimal TotalPrice { get; set;}

        [DisplayName("Delivery Time")]
        public DateTime? DeliveryDate { get; set;}
        [DisplayName("Order Time")]
        public DateTime? CheckOutDate { get; set;}
        
        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        [ForeignKey("Customer")]
        public string CustomerId { get; set; } = string.Empty;

        public virtual Customer Customer { get; set; } = new();

        [ForeignKey("Review")]
        public int? ReviewId { get; set; }

        public virtual Review Review { get; set; } = new();

        [ForeignKey("Seller")]
        public string SellerId { get; set; } = string.Empty;

        public virtual Seller Seller { get; set; } = new();

        [ForeignKey("Address")]
        public int AddressId { get; set; }

        public virtual Address Address { get; set; } = new();

        [ForeignKey("PromoCode")]
        public int? PromoCodeId { get; set; }

        public virtual PromoCode? PromoCode { get; set; }

        [ForeignKey("Payment")]
        public int? PaymentId { get; set; }

        public Payment? Payment { get; set; }

        public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
    }
}

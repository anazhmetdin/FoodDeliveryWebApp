﻿using FoodDeliveryWebApp.Areas.Identity.Data;
using FoodDeliveryWebApp.Models.Enums;
using Stripe;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryWebApp.Models
{
    public class Order : BaseModel
    {
        public decimal TotalPrice { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        [ForeignKey("Customer")]
        public string CustomerId { get; set; } = string.Empty;

        public virtual Customer Customer { get; set; } = new();

        public virtual Review Review { get; set; } = new();

        public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public int? PromoCodeId { get; set; }
        public virtual PromoCode? PromoCode { get; set; }

        [ForeignKey("Payment")]
        public string? PaymentId { get; set; } = string.Empty;
        public virtual Payment? Payment { get; set; } = new();
    }
}

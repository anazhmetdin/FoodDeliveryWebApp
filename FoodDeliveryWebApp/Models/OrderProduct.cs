using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryWebApp.Models
{
    public class OrderProduct
    {
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }

        [Range(minimum: 1, maximum: int.MaxValue)]
        public int Quantity { get; set; }

        public virtual Product Product { get; set; } = new();

        public virtual Order Order { get; set; } = new();
        [Precision(2)]
        public decimal UnitPrice { get; set; }
    }
}

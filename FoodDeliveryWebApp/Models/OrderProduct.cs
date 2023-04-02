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
        public int Quantity { get; set; }
        public Product? Product { get; set; }
        public Order? Order { get; set; }

        [Range(0, int.MaxValue)]
        public decimal UnitPrice { get; set; } = 0;
    }
}

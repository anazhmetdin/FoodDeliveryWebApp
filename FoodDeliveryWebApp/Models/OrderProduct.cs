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

        public decimal UnitPrice { get; set; }

        public Product Product { get; set; } = new();

        public Order Order { get; set; } = new();
    }
}

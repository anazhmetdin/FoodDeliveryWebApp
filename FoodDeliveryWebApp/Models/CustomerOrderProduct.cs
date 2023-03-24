using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryWebApp.Models
{
    public class CustomerOrderProduct: BaseModel
    {
        [ForeignKey("Customer")]
        public string CustomerId { get; set; } = string.Empty;

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }

        [Range(minimum: 1, maximum: int.MaxValue)]
        public int Quantity { get; set; }

        public virtual Customer Customer { get; set; } = new();

        public virtual Product Product { get; set; } = new();

        public virtual Order Order { get; set; } = new();
    }
}

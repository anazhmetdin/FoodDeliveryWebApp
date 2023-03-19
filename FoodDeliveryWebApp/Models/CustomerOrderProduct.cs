using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryWebApp.Models
{
    public class CustomerOrderProduct
    {
        //[ForeignKey("CustomerId")]
        //public Customer Customer { get; set; }
        //[ForeignKey("ProductId")]
        //public Product Product { get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; }
        [Range(minimum:1 , maximum:int.MaxValue)]
        public int Quantity { get; set; }
    }
}

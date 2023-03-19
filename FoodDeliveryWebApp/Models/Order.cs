using System.ComponentModel.DataAnnotations;

namespace FoodDeliveryWebApp.Models
{
    public class Order:BaseModel
    {
        public ICollection<Product> Products { get; set; }
        public decimal TotalPrice { get; set;}
        [DataType(DataType.Date)]
        public DateTime DeliveryDate { get; set;}
        [DataType(DataType.Date)]
        public DateTime CheckOutDate { get; set;}
    }
}

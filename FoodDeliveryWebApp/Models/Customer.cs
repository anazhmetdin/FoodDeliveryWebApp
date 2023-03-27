using FoodDeliveryWebApp.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryWebApp.Models
{
    public class Customer: BaseModel
    {
        [Key]
        [ForeignKey("User")]
        public string UserId { get; set; } = string.Empty;

        public virtual AppUser User { get; set; } = new();

        public virtual ICollection<CustomerOrderProduct> CustomerOrderProducts { get; set; } = new List<CustomerOrderProduct>();
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}

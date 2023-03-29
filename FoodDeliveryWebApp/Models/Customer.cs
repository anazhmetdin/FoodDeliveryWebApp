using FoodDeliveryWebApp.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryWebApp.Models
{
    public class Customer
    {
        [Key]
        [ForeignKey("User")]
        public string Id { get; set; } = string.Empty;

        public virtual AppUser User { get; set; } = new();

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}

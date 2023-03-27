using FoodDeliveryWebApp.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryWebApp.Models
{
    public class Address: BaseModel
    {

        [Required]
        public string StreetName { get; set; } = string.Empty;

        [Required]
        public string BuildingNumber { get; set; } = string.Empty;

        [Required]
        public string City { get; set; } = string.Empty;

        [Required]
        public string Region { get; set; } = string.Empty;

        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; } = string.Empty;

        public virtual AppUser User { get; set; } = new();
    }
}
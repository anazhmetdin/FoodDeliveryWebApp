using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FoodDeliveryWebApp.Areas.Identity.Data;
using FoodDeliveryWebApp.Models.Enums;

namespace FoodDeliveryWebApp.Models
{
    public class PromoCode : BaseModel
    {
        [Range(0,1)]
        public double Discount { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [ForeignKey("Seller")]
        public string SellerId { get; set; } = string.Empty;

        public virtual AppUser Seller { get; set; } = new();

        public virtual ICollection<Product> AppliedTo { get; set; } = new List<Product>();
    }
}

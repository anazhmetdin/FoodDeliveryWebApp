using FoodDeliveryWebApp.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryWebApp.Models
{
    public class PromoCode : BaseModel
    {
        [Range(0, 1)]
        public double Discount { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public decimal MaximumDiscount { get; set; }

        public virtual ICollection<Category>? AppliedTo { get; set; } = new List<Category>();

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

        [MaxLength(16)]
        public string Code { get; set; }
    }
}

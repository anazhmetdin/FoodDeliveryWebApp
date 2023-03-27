using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FoodDeliveryWebApp.Areas.Identity.Data;
using FoodDeliveryWebApp.Models.Categories;
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

        public virtual ICollection<Category> AppliedTo { get; set; } = new List<Category>();

        [Range(0, 100)]
        public int Sale { get; set; }
        [Range(0, 1000000)]
        public int MaxSale { get; set; }
    }
}

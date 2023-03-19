using System.ComponentModel.DataAnnotations;

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

        //public ICollection<Category> AppliedTo { get; set; } = new List<Category>;
    }
}

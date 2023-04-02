using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryWebApp.Models
{
    public class TrendingSeller: BaseModel
    {
        [ForeignKey("Seller")]
        public new string Id { get; set; }
        public virtual Seller Seller { get; set; }
    }
}

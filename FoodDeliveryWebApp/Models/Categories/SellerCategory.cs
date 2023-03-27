using FoodDeliveryWebApp.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryWebApp.Models.Categories
{
    public class SellerCategory: BaseModel
    {
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [ForeignKey("Seller")]
        public string SellerId { get; set; }
        public Seller Seller { get; set; }
    }
}

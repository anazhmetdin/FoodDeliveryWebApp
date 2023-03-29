using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryWebApp.Models
{
    public class SellerCategories
    {
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        
        [ForeignKey("Seller")]
        public string SellerId { get; set; } = string.Empty;

        public Category Category { get; set; } = new();

        public Seller Seller { get; set; } = new();
    }
}

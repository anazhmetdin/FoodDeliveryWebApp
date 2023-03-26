using FoodDeliveryWebApp.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryWebApp.Models.Categories
{
    public class SellerCategory
    {
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public string SellerId { get; set; }
        public Seller Seller { get; set; }
    }
}

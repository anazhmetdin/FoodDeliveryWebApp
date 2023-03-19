using FoodDeliveryWebApp.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryWebApp.Models
{
    public enum Category
    {
        Fried_Chicken, Pizza, Sushi, Asian, Shawerma, Burger, Sea_Food, Oriental, Dessert, Street_Food, Fine_Cuisine, Italian
    }
    public class Product: BaseModel
    {
        [MaxLength(128)]
        [Required]
        public string Name { get; set; } = string.Empty;
        
        [MaxLength(512)]
        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Range(0, 100000)]
        public decimal Price { get; set; }
        
        [Required]
        public bool InStock { get; set; }

        [Required]
        [ForeignKey("Seller")]
        public string SellerId { get; set; }  = string.Empty;
        
        public virtual AppUser? Seller { get; set; }
        
        public Category Category { get; set; }
        
        public byte[] Image { get; set; } = new byte[256];
    }
}

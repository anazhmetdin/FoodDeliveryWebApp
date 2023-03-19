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
        public string Name { get; set; }
        [MaxLength(512)]
        [Required]
        public string Description { get; set; }
        [Required]
        [Range(0, 100000)]
        public decimal Price { get; set; }
        [Required]
        public bool InStock { get; set; }
        [Required]
        [ForeignKey("Seller")]
        public int AppUserId;
        public virtual AppUser? Seller { get; set; }
        public Category Category { get; set; }
    }
}

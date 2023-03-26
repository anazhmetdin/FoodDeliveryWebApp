using FoodDeliveryWebApp.Areas.Identity.Data;
using FoodDeliveryWebApp.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FoodDeliveryWebApp.Models.Categories;

namespace FoodDeliveryWebApp.Models
{
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

        public byte[] Image { get; set; } = new byte[256];
        
        [Required]
        [ForeignKey("Seller")]
        public string SellerId { get; set; }  = string.Empty;
        
        public virtual Seller? Seller { get; set; }

        public virtual ICollection<CustomerOrderProduct> CustomerOrderProducts { get; set; } = new List<CustomerOrderProduct>();
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}

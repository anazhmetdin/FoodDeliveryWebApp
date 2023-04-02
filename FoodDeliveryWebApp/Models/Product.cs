using FoodDeliveryWebApp.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryWebApp.Models
{
    public class Product : BaseModel
    {
        [MaxLength(128)]
        [Required]
        public string Name { get; set; } = string.Empty;

        [MaxLength(512)]
        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Range(0, int.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        public bool InStock { get; set; }

        public byte[]? Image { get; set; } = new byte[256];

        [Range(0, 100)]
        public int Sale { get; set; } = 0;

        public bool HasSale { get; set; }


        [Required]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public virtual Category? Category { get; set; }

        [Required]
        [ForeignKey("Seller")]
        public string SellerId { get; set; } = string.Empty;

        public virtual Seller? Seller { get; set; }

        public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();

        [NotMapped]
        public decimal SalePrice { get => Price * (100 - Sale) / 100; }
    }
}

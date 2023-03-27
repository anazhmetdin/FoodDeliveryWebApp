using FoodDeliveryWebApp.Areas.Identity.Data;
using FoodDeliveryWebApp.Models.Categories;
using FoodDeliveryWebApp.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryWebApp.Models
{
    public class Seller: BaseModel
    {
        [Key]
        [ForeignKey("User")]
        public string UserId { get; set; } = string.Empty;

        [Required]
        [StringLength(30)]
        public string StoreName { get; set; } = string.Empty;

        [NotMapped]
        public int BranchesNumber { get => User.Addresses.Count; }

        [Required]
        public AppUser User { get; set; } = new();
        public virtual ICollection<Category> Categories { get; set; }
        public byte[] Logo { get; set; } = new byte[256];
    }
}

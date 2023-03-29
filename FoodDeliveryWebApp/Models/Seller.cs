using FoodDeliveryWebApp.Areas.Identity.Data;
using FoodDeliveryWebApp.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryWebApp.Models
{
    public class Seller:BaseModel
    {
        [Key]
        [ForeignKey("User")]
        public new string Id { get; set; } = string.Empty;

        [Required]
        [StringLength(30)]
        public string StoreName { get; set; } = string.Empty;

        [NotMapped]
        public int BranchesNumber { get => User.Addresses.Count; }
        
        [Required]
        public byte[] Logo { get; set; } = new byte[256];

        public virtual AppUser User { get; set; } = new();

        public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

        public SellerStatus Status { get; set; } = SellerStatus.UnderReview;
    }
}

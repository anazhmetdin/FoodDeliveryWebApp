using FoodDeliveryWebApp.Areas.Identity.Data;
using FoodDeliveryWebApp.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryWebApp.Models
{
    public class Seller : BaseModel
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
        public byte[]? Logo { get; set; }

        public virtual AppUser User { get; set; } = new();

        public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();

        public SellerStatus Status { get; set; } = SellerStatus.UnderReview;

        [Range(0, 5)]
        public int Rate { get; set; } = 0;
    }
}

using FoodDeliveryWebApp.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace FoodDeliveryWebApp.Areas.Admin.ViewModels
{
    public class SellerViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Store Name")]
        [Required]
        [StringLength(30)]
        public string StoreName { get; set; } = string.Empty;

        //[Display(Name = "Logo")]
        //[Required(ErrorMessage = "Please select a logo.")]
        //public byte[] Logo { get; set; } = new byte[256];

        [Display(Name = "Status")]
        public SellerStatus Status { get; set; } = SellerStatus.UnderReview;

        [Display(Name = "User ID")]
        public string UserId { get; set; }
    }
}

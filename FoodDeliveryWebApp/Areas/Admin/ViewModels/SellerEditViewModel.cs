using FoodDeliveryWebApp.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace FoodDeliveryWebApp.Areas.Admin.ViewModels
{
    public class SellerEditViewModel
    {
        public string Id { get; set; }

        [Required]
        [StringLength(30)]
        public string StoreName { get; set; } = string.Empty;

        [Required]
        public byte[] Logo { get; set; } = new byte[256];

        public SellerStatus Status { get; set; } = SellerStatus.UnderReview;
        //public string? Id { get; set; }
        //public string? StoreName { get; set; }
        //public SellerStatus? Status { get; set; }
    }
}
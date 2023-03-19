using System.ComponentModel.DataAnnotations;

namespace FoodDeliveryWebApp.Models
{
    public class Review : BaseModel
    {
        [Range(1,5)]
        public int Rate { get; set; }

        [MaxLength(100)]
        public string UserReview { get; set; } = string.Empty;
    }
}

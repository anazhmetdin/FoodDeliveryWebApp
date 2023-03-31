using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryWebApp.Models
{
    public class Review : BaseModel
    {
        [Range(1,5)]
        public int Rate { get; set; }

        [MaxLength(100)]
        public string UserReview { get; set; } = string.Empty;
        
        [ForeignKey("Customer")]
        public string CustomerId { get; set; }
        
        public virtual Customer Customer { get; set; }
        
        [ForeignKey("Seller")]
        public string SellerId { get; set; }
        
        public virtual Seller Seller { get; set; }

        //[ForeignKey("Order")]
        //public int OrderId { get; set; }

        //public virtual Order Order { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace FoodDeliveryWebApp.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }

        public string Status { get; set; } = string.Empty;

        public string? StripeId { get; set; }

        [DataType(DataType.Currency)]
        public long? Amount { get; set; }
        [DataType(DataType.Currency)]
        public long? AmountReceived { get; set; }
    }
}

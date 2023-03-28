using System.ComponentModel.DataAnnotations;

namespace FoodDeliveryWebApp.Models
{
    public class Payment
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Status { get; set; } = string.Empty;

        [DataType(DataType.Currency)]
        public long? Amount { get; set; }
        [DataType(DataType.Currency)]
        public long? AmountReceived { get; set; }
    }
}

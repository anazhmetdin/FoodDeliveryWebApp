using Newtonsoft.Json;

namespace FoodDeliveryWebApp.Models
{
    public class PaymentIntentCreateRequest
    {
        [JsonProperty("items")]
        public Item[] Items { get; set; }
    }
}

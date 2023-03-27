using Newtonsoft.Json;

namespace FoodDeliveryWebApp.Models
{
    public class Item
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}

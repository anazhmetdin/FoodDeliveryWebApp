using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FoodDeliveryWebApp.Models
{
    public class Category : BaseModel
    {
        public string Name { get; set; } = string.Empty;

        public virtual ICollection<Seller> Sellers { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}

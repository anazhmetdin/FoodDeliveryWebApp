using System.ComponentModel.DataAnnotations;

namespace FoodDeliveryWebApp.Models.Categories
{
    public class Category : BaseModel
    {
        [MaxLength(32)]
        public string Name { get; set; }
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
        public virtual ICollection<Seller> Sellers { get; set;} = new List<Seller>();
    }
}

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FoodDeliveryWebApp.Models
{
    public class Category:BaseModel
    {
        public string Name { get; set; } = string.Empty;

        public virtual ICollection<SellerCategories> SellerCategories { get; set; } = new List<SellerCategories>();
    }
}

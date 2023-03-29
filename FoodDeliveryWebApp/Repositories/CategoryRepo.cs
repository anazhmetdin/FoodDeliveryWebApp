using FoodDeliveryWebApp.Data;
using FoodDeliveryWebApp.Models;

namespace FoodDeliveryWebApp.Repositories
{
    public class CategoryRepo : ModelRepo<Category>
    {
        public CategoryRepo(FoodDeliveryWebAppContext context) : base(context)
        {
        }
    }
}

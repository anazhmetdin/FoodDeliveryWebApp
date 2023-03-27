using FoodDeliveryWebApp.Data;
using FoodDeliveryWebApp.Models.Categories;

namespace FoodDeliveryWebApp.Repositories
{
    public class CategoryRepo : ModelRepo<Category>
    {
        public CategoryRepo(FoodDeliveryWebAppContext context) : base(context)
        {
        }
    }
}

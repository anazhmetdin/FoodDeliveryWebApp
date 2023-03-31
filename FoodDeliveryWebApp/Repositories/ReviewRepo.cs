using FoodDeliveryWebApp.Data;
using FoodDeliveryWebApp.Models;
using Stripe;
using Review = FoodDeliveryWebApp.Models.Review;

namespace FoodDeliveryWebApp.Repositories
{
    public class ReviewRepo : ModelRepo<Review>
    {
        public ReviewRepo(FoodDeliveryWebAppContext context) : base(context)
        {
        }
        //public virtual List<Review> GetAll()
        //{
        //    return Context.Set<Review>().ToList();
        //}
    }
}

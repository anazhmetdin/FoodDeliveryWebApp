using FoodDeliveryWebApp.Data;
using FoodDeliveryWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryWebApp.Repositories
{
    public class PromoCodeRepo : ModelRepo<PromoCode>, IPromoCodeRepo
    {
        public PromoCodeRepo(FoodDeliveryWebAppContext context) : base (context)
        {
        }
    }
}

using FoodDeliveryWebApp.Models;
using FoodDeliveryWebApp.Repositories;

namespace FoodDeliveryWebApp.Contracts
{
    public interface ITrendingSellerRepo : IModelRepo<TrendingSeller>
    {
        public bool TryUpdateAll(List<string> sellers);
    }
}

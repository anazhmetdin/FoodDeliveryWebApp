using FoodDeliveryWebApp.Areas.Identity.Data;
using FoodDeliveryWebApp.Models;
using FoodDeliveryWebApp.ViewModels;

namespace FoodDeliveryWebApp.Contracts
{
    public interface ICustomerRestaurantsRepo
    {
        public ICollection<AppUser> GetSellers();

        public ICollection<ProductViewModel> GetSellerProducts(string sellerId);

        public ICollection<AppUser> GetSellersFiltered(Func<AppUser, bool> func);
    }
}

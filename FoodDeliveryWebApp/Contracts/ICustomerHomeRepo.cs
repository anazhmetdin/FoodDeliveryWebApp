using FoodDeliveryWebApp.Areas.Identity.Data;
using FoodDeliveryWebApp.Models;

namespace FoodDeliveryWebApp.Contracts
{
    public interface ICustomerHomeRepo
    {
        public ICollection<AppUser> GetSellers();

        public ICollection<Product> GetSellerProducts(string sellerId);

        public ICollection<AppUser> GetSellersFiltered(Func<AppUser, bool> func);
    }
}

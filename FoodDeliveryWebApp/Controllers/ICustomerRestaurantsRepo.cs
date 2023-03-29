using FoodDeliveryWebApp.Areas.Identity.Data;
using FoodDeliveryWebApp.Models;
using FoodDeliveryWebApp.ViewModels;

namespace FoodDeliveryWebApp.Contracts
{
    public interface ICustomerRestaurantsRepo
    {
        public ICollection<SellerViewModel> GetSellers();

        public ICollection<ProductViewModel> GetSellerProducts(string sellerId);

        public ICollection<SellerViewModel> GetSellersFiltered(List<Category> categories);
    }
}
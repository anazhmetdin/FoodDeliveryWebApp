using FoodDeliveryWebApp.Areas.Identity.Data;
using FoodDeliveryWebApp.Models;
using FoodDeliveryWebApp.ViewModels;

namespace FoodDeliveryWebApp.Contracts
{
    public interface ICustomerRestaurantsRepo
    {

        public ICollection<SellerViewModel> GetSellers();

        public ICollection<ProductViewModel> GetSellerProducts(string sellerId);

        public ICollection<SellerViewModel> GetSellersFiltered(List<Category> categoriesbool, bool hasPromo, bool orderAlpha, bool orderRate);

        public ICollection<SellerViewModel> GetSellersSearched(string text);
    
        public Customer? GetCustomer(string customerId);
    }
}
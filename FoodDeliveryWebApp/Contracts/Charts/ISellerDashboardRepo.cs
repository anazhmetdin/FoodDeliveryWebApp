using FoodDeliveryWebApp.ViewModels.Charts;

namespace FoodDeliveryWebApp.Contracts.Charts
{
    public interface ISellerDashboardRepo
    {
        SellerDashboard GetSellerDashboard(string sellerId, int? year);
    }
}

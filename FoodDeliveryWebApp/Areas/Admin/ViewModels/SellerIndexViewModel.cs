namespace FoodDeliveryWebApp.Areas.Admin.ViewModels
{
    public class SellerIndexViewModel
    {
        public List<Models.Seller>? Sellers { get; set; }
        public string? SelectedStatus { get; set; }
        public string? SelectedSort { get; set; }
    }
}
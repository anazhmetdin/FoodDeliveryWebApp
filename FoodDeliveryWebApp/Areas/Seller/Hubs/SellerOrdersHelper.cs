using FoodDeliveryWebApp.Contracts;
using FoodDeliveryWebApp.Models;
using FoodDeliveryWebApp.Models.Enums;
using FoodDeliveryWebApp.ViewModels;

namespace FoodDeliveryWebApp.Areas.Seller.Hubs
{
    public class SellerOrdersHelper
    {
        public static SellerOrdersIndexViewModel GetActiveOrders(string SellerId, ISellerRepo _sellerRepo)
        {
            var posted = _sellerRepo.GetOrders(SellerId, OrderStatus.Posted);
            var inprogress = _sellerRepo.GetOrders(SellerId, OrderStatus.InProgress);

            var Model = new SellerOrdersIndexViewModel()
            {
                PostedOrders = new()
                {
                    Oders = posted,
                    Buttons = new() { SellerOrderButtons.Accept, SellerOrderButtons.Cancel }
                },
                InProgressOrders = new()
                {
                    Oders = inprogress,
                    Buttons = new() { SellerOrderButtons.Delivered, SellerOrderButtons.Cancel }
                }
            };

            return Model;
        }
    }
}

using FoodDeliveryWebApp.Areas.Identity.Data;
using FoodDeliveryWebApp.Contracts;
using FoodDeliveryWebApp.Models;
using FoodDeliveryWebApp.Models.Enums;
using FoodDeliveryWebApp.RazorRenderer;
using FoodDeliveryWebApp.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using static System.Net.WebRequestMethods;

namespace FoodDeliveryWebApp.Hubs
{
    [Authorize(Roles="Seller")]
    public class SellerOrdersIndexHub:  Hub
    {
        private readonly IServiceProvider _serviceProvider;
        public SellerOrdersIndexHub(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public async Task SendOrders()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _SellerRepo = scope.ServiceProvider.GetRequiredService<ISellerRepo>();
                var _userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                var _renderer = scope.ServiceProvider.GetRequiredService<IRazorPartialToStringRenderer>();

                if (Context.User != null)
                {
                    var SellerId = _userManager.GetUserId(Context.User);
                    if (SellerId != null)
                    {
                        var partial = "~/Areas/Seller/Views/Shared/_SellerOrderIndex.cshtml";

                        string PostedProducts = await _renderer.RenderPartialToStringAsync(partial,
                            _SellerRepo.GetOrders(SellerId, OrderStatus.Posted));
                        
                        string InProgressProducts = await _renderer.RenderPartialToStringAsync(partial,
                            _SellerRepo.GetOrders(SellerId, OrderStatus.InProgress));

                        await Clients.User(SellerId).SendAsync("ReceivedOrders", PostedProducts, InProgressProducts);
                    }
                }
            }
        }
    }
}

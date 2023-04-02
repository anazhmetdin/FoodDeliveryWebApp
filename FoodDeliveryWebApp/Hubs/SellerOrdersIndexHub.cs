using FoodDeliveryWebApp.Areas.Identity.Data;
using FoodDeliveryWebApp.Areas.Seller.Hubs;
using FoodDeliveryWebApp.Contracts;
using FoodDeliveryWebApp.Models;
using FoodDeliveryWebApp.Models.Enums;
using FoodDeliveryWebApp.RazorRenderer;
using FoodDeliveryWebApp.Repositories;
using FoodDeliveryWebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using static System.Net.WebRequestMethods;

namespace FoodDeliveryWebApp.Hubs
{
    [Authorize(Roles = "Seller")]
    public class SellerOrdersIndexHub : Hub
    {
        private readonly IServiceProvider _serviceProvider;
        public SellerOrdersIndexHub(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public async Task SendOrders()
        {

            if (Context == null)
            {
                return;
            }

            using (var scope = _serviceProvider.CreateScope())
            {
                var _SellerRepo = scope.ServiceProvider.GetRequiredService<ISellerRepo>();
                var _userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                var _renderer = scope.ServiceProvider.GetRequiredService<IRazorPartialToStringRenderer>();
                var httpContext = Context.GetHttpContext();
                if (Context.User != null && httpContext != null)
                {
                    var SellerId = _userManager.GetUserId(Context.User);
                    if (SellerId != null)
                    {
                        var partial = "_SellerOrderIndex";

                        var Model = SellerOrdersHelper.GetActiveOrders(SellerId, _SellerRepo);

                        string PostedProducts = await _renderer.RenderPartialToStringAsync(partial,
                            Model.PostedOrders, httpContext);

                        string InProgressProducts = await _renderer.RenderPartialToStringAsync(partial,
                            Model.InProgressOrders, httpContext);

                        await Clients.User(SellerId).SendAsync("ReceivedOrders", PostedProducts,
                            InProgressProducts, Model.PostedOrders.Oders.Count());
                    }
                }
            }
        }
    }
}

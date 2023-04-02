using FoodDeliveryWebApp.Areas.Identity.Data;
using FoodDeliveryWebApp.Areas.Seller.Hubs;
using FoodDeliveryWebApp.Contracts;
using FoodDeliveryWebApp.Data;
using FoodDeliveryWebApp.Models;
using FoodDeliveryWebApp.Models.Enums;
using FoodDeliveryWebApp.Repositories;
using FoodDeliveryWebApp.SubscribeTableDependencies;
using FoodDeliveryWebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDeliveryWebApp.Areas.Seller.Controllers
{
    [Area("Seller")]
    [Authorize(Roles = "Seller")]
    [AutoValidateAntiforgeryToken]
    public class OrdersController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ISellerRepo _sellerRepo;

        public OrdersController(ISellerRepo sellerRepo, UserManager<AppUser> userManager)
        {
            _sellerRepo = sellerRepo;
            _userManager = userManager;
        }

        // GET: Seller/Orders
        public IActionResult Index()
        {
            var SellerId = _userManager.GetUserId(User);

            var Model = SellerOrdersHelper.GetActiveOrders(SellerId ?? "", _sellerRepo);

            return View(Model);
        }

        // GET: Seller/Orders
        public IActionResult Archive()
        {
            var SellerId = _userManager.GetUserId(User);

            var DeliveredOrders = _sellerRepo.GetOrders(SellerId, OrderStatus.Delivered);
            var RejectedOrders = _sellerRepo.GetOrders(SellerId, OrderStatus.Rejected);

            SellerOrderArchiveViewModel Model = new SellerOrderArchiveViewModel()
            {
                DeliveredOrders = DeliveredOrders,
                RejectedOrders = RejectedOrders
            };

            return View(Model);
        }

        // GET: Seller/Orders/Details/5
        public IActionResult Details(int? id)
        {
            var SellerId = _userManager.GetUserId(User);
            var order = _sellerRepo.GetOrder(id, SellerId);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        [HttpGet]
        public IActionResult ChangeStatus(int? id, OrderStatus? status)
        {
            var SellerId = _userManager.GetUserId(User);
            if (_sellerRepo.ChangeOrderStatus(id, SellerId, status))
                return Ok();

            return BadRequest();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoodDeliveryWebApp.Data;
using FoodDeliveryWebApp.Contracts;
using Microsoft.AspNetCore.Identity;
using FoodDeliveryWebApp.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using FoodDeliveryWebApp.ViewModels;
using FoodDeliveryWebApp.SubscribeTableDependencies;
using FoodDeliveryWebApp.Models.Enums;
using System.Composition;

namespace FoodDeliveryWebApp.Areas.Seller.Controllers
{

    [Area("Seller")]
    [Authorize(Roles = "Seller")]
    [AutoValidateAntiforgeryToken]
    public class OrdersController : Controller
    {
        private readonly FoodDeliveryWebAppContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly ISellerRepo _sellerRepo;

        public OrdersController(FoodDeliveryWebAppContext context, ISellerRepo sellerRepo, UserManager<AppUser> userManager)
        {
            _context = context;
            _sellerRepo = sellerRepo;
            _userManager = userManager;
        }

        // GET: Seller/Orders
        public IActionResult Index()
        {
            return View();
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

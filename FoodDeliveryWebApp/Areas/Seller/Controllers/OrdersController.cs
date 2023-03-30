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
            var SellerId = _userManager.GetUserId(User);
            var PostedOrders = _sellerRepo.GetOrders(SellerId, Models.Enums.OrderStatus.Posted);
            var InProgressOrders = _sellerRepo.GetOrders(SellerId, Models.Enums.OrderStatus.InProgress);

            var Model = new SellerOrdersViewModel()
            {
                PostedOrders = PostedOrders,
                InProgressOrders = InProgressOrders
            };

            var x = SubscribeOrderTableDependency.tableDependency;

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

        // GET: Seller/Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.PromoCode)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Seller/Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Orders == null)
            {
                return Problem("Entity set 'FoodDeliveryWebAppContext.Orders'  is null.");
            }
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

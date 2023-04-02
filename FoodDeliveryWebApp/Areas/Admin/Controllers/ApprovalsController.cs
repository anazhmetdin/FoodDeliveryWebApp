using FoodDeliveryWebApp.Areas.Admin.ViewModels;
using FoodDeliveryWebApp.Constants;
using FoodDeliveryWebApp.Data;
using FoodDeliveryWebApp.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ApprovalsController : Controller
    {
        private readonly FoodDeliveryWebAppContext _context;

        public ApprovalsController(FoodDeliveryWebAppContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var sellers = await _context.Sellers
                .Select(seller => new SellerViewModel
                {
                    Id = seller.Id,
                    StoreName = seller.StoreName,
                    Status = seller.Status
                })
                .ToListAsync();

            return View(sellers);
        }

        public async Task<IActionResult> Approve(string id)
        {
            var seller = await _context.Sellers.FindAsync(id);
            seller.Status = SellerStatus.Accepted;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Reject(string id)
        {
            var seller = await _context.Sellers.FindAsync(id);
            seller.Status = SellerStatus.Rejected;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}

using FoodDeliveryWebApp.Areas.Admin.ViewModels;
using FoodDeliveryWebApp.Data;
using FoodDeliveryWebApp.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SellerController : Controller
    {
        private readonly FoodDeliveryWebAppContext _context;

        public SellerController(FoodDeliveryWebAppContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var sellers = await _context.Sellers.ToListAsync(); 
            return View(sellers);
        }

        //[HttpGet]
        //public async Task<IActionResult> Edit(string id)
        //{
        //    var seller = await _context.Sellers.FindAsync(id);
        //    if (seller == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(seller);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> EditStatus(string id, SellerStatus status)
        //{
        //    var seller = await _context.Sellers.FindAsync(id);
        //    if (seller == null)
        //    {
        //        return NotFound();
        //    }
        //    seller.Status = status;
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var seller = await _context.Sellers.FindAsync(id);
            if (seller == null)
            {
                return NotFound();
            }
            var viewModel = new SellerEditViewModel
            {
                Id = seller.Id,
                StoreName = seller.StoreName,
                Logo = seller.Logo,
                Status = seller.Status
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,StoreName,Status,Logo")] SellerEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var seller = await _context.Sellers.FindAsync(model.Id);
                if (seller == null)
                {
                    return NotFound();
                }
                seller.StoreName = model.StoreName;
                seller.Logo = model.Logo;
                seller.Status = model.Status;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);

        }
    }
}

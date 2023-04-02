using FoodDeliveryWebApp.Areas.Admin.ViewModels;
using FoodDeliveryWebApp.Constants;
using FoodDeliveryWebApp.Data;
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

        public async Task<IActionResult> Edit(string id)
        {
            var seller = await _context.Sellers
                .Include(s => s.User)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (seller == null)
            {
                return NotFound();
            }

            var viewModel = new SellerViewModel
            {
                Id = seller.Id,
                StoreName = seller.StoreName,
                Status = seller.Status,
                UserId = seller.User.Id,
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, SellerViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var seller = await _context.Sellers
                    .Include(s => s.User)
                    .FirstOrDefaultAsync(s => s.Id == id);

                if (seller == null)
                {
                    return NotFound();
                }

                seller.StoreName = viewModel.StoreName;
                seller.Status = viewModel.Status;

                _context.Update(seller);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }
    }
}

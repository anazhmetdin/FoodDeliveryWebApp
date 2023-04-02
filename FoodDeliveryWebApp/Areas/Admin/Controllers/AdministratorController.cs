using FoodDeliveryWebApp.Areas.Admin.ViewModels;
using FoodDeliveryWebApp.Contracts;
using FoodDeliveryWebApp.Data;
using FoodDeliveryWebApp.Models;
using FoodDeliveryWebApp.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;
using System.Web.Http.ModelBinding;

namespace FoodDeliveryWebApp.Areas.Admin.Controllers
{
    // /admin/Administrator/index
    [Area("Admin")]
    public class AdministratorController : Controller
    {
        private readonly ISellerRepo _sellerRepo;
        private readonly FoodDeliveryWebAppContext _context;

        public AdministratorController(ISellerRepo sellerRepo, FoodDeliveryWebAppContext context)
        {
            _sellerRepo = sellerRepo;
            _context = context;
        }

        //public async Task<IActionResult> Index(string status, string sort)
        //{
        //    var sellers = _context.Sellers.AsQueryable();

        //    if (!string.IsNullOrEmpty(status))
        //    {
        //        var statusFlags = (SellerStatus)Enum.Parse(typeof(SellerStatus), status);
        //        sellers = sellers.Where(s => s.Status.HasFlag(statusFlags));
        //    }

        //    if (!string.IsNullOrEmpty(sort))
        //    {
        //        switch (sort)
        //        {
        //            case "id":
        //                sellers = sellers.OrderBy(s => s.Id);
        //                break;
        //            case "storename":
        //                sellers = sellers.OrderBy(s => s.StoreName);
        //                break;
        //            case "status":
        //                sellers = sellers.OrderBy(s => s.Status);
        //                break;
        //            default:
        //                break;
        //        }
        //    }

        //    var viewModel = new SellerIndexViewModel
        //    {
        //        Sellers = await sellers.ToListAsync(),
        //        SelectedStatus = status,
        //        SelectedSort = sort
        //    };

        //    return View(viewModel);
        //}
        //public async Task<IActionResult> Edit(string id)
        //{
        //    if (string.IsNullOrEmpty(id))
        //    {
        //        return NotFound();
        //    }

        //    var seller = await _context.Sellers.FirstOrDefaultAsync(s => s.Id == id);
        //    if (seller == null)
        //    {
        //        return NotFound();
        //    }

        //    var viewModel = new SellerEditViewModel
        //    {
        //        Id = seller.Id,
        //        StoreName = seller.StoreName,
        //        Status = seller.Status
        //    };

        //    return View(viewModel);
        //}

        //// POST: Seller/Edit/abc123
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(string Id, SellerEditViewModel viewModel)
        //{
        //    if (string.IsNullOrEmpty(Id) || Id != viewModel.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        var seller = await _context.Sellers.FirstOrDefaultAsync(s => s.Id == Id);
        //        if (seller == null)
        //        {
        //            return NotFound();

        //        }

        //        seller.Status = viewModel.Status ?? SellerStatus.UnderReview;

        //        _context.Update(seller);
        //        await _context.SaveChangesAsync();

        //        return RedirectToAction(nameof(Index));
        //    }

        //    return View(viewModel);
        //}
    }
}

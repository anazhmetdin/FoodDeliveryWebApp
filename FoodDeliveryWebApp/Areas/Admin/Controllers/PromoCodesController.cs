using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoodDeliveryWebApp.Data;
using FoodDeliveryWebApp.Models;
using FoodDeliveryWebApp.Constants;
using FoodDeliveryWebApp.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace FoodDeliveryWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class PromoCodesController : Controller
    {
        private readonly IPromoCodeRepo _repo;

        public PromoCodesController(IPromoCodeRepo repo)
        {
            _repo = repo;
        }

        // GET: Admin/PromoCodes
        public async Task<IActionResult> Index()
        {
            var promocodes = _repo.GetAll();
            return View(promocodes);
        }

        //// GET: Admin/PromoCodes/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null || _context.PromoCodes == null)
        //    {
        //        return NotFound();
        //    }

        //    var promoCode = await _context.PromoCodes
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (promoCode == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(promoCode);
        //}

        // GET: Admin/PromoCodes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/PromoCodes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Discount,StartDate,EndDate,MaximumDiscount,Code,Id")] PromoCode promoCode)
        {
            if (ModelState.IsValid)
            {
                _repo.TryInsert(promoCode);
                return RedirectToAction("Index");
            }
            return View(promoCode);
        }

        // GET: Admin/PromoCodes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var code = _repo.GetById(id);
            return View(code);
        }

        // POST: Admin/PromoCodes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Discount,StartDate,EndDate,MaximumDiscount,Code,Id")] PromoCode promoCode)
        {
            if (id != promoCode.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                //try
                //{
                    _repo.TryUpdate(promoCode);
                    return RedirectToAction("Index");
                //}
                //catch (DbUpdateConcurrencyException)
                //{
                //    if (!PromoCodeExists(promoCode.Id))
                //    {
                //        return NotFound();
                //    }
                //    else
                //    {
                //        throw;
                //    }
                //}
            }
            return View(promoCode);
        }

        // GET: Admin/PromoCodes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            
            //if (id == null || _context.PromoCodes == null)
            //{
            //    return NotFound();
            //}

            //var promoCode = await _context.PromoCodes
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (promoCode == null)
            //{
            //    return NotFound();
            //}
            _repo.TryDelete(id);
            return RedirectToAction("Index");
        }

        //// POST: Admin/PromoCodes/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.PromoCodes == null)
        //    {
        //        return Problem("Entity set 'FoodDeliveryWebAppContext.PromoCodes'  is null.");
        //    }
        //    var promoCode = await _context.PromoCodes.FindAsync(id);
        //    if (promoCode != null)
        //    {
        //        _context.PromoCodes.Remove(promoCode);
        //    }
            
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool PromoCodeExists(int id)
        //{
        //  return (_context.PromoCodes?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}

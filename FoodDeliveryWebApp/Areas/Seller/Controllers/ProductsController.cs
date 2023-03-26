using FoodDeliveryWebApp.Areas.Identity.Data;
using FoodDeliveryWebApp.Contracts;
using FoodDeliveryWebApp.Models;
using FoodDeliveryWebApp.Models.Enums;
using FoodDeliveryWebApp.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace FoodDeliveryWebApp.Areas.Seller.Controllers
{
    [Authorize(Roles = "Seller")]
    [Area("Seller")]
    public class ProductsController : Controller
    {
        private readonly ISellerRepo _sellerRepo;
        private readonly UserManager<AppUser> _userManager;

        public ProductsController(ISellerRepo sellerRepo, UserManager<AppUser> userManager)
        {
            _sellerRepo = sellerRepo;
            _userManager = userManager;
        }

        // GET: Seller/Products
        public ActionResult Index()
        {
            var sellerId = _userManager.GetUserId(User);

            return View(_sellerRepo.GetSellerProducts(sellerId));
        }

        // GET: SellerController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SellerController/Create
        public ActionResult Create()
        {
            var sellerId = _userManager.GetUserId(User);
            ViewBag.sell = sellerId;
            return View();
        }

        // POST: SellerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Name,Description,Price,InStock,Image ,SellerId")] Product product , IFormFile Image)
        {
            var sellerId = _userManager.GetUserId(User);
            ViewBag.sell = sellerId;
            try
            {
                if (ModelState.IsValid)
                {
                    //ViewBag.CategoryList = new SelectList(Enum.GetValues(typeof(Category)));
                    //product.SellerId = sellerId;
                    using (var stream = new MemoryStream())
                    {
                        await Image.CopyToAsync(stream);
                        product.Image = stream.ToArray();
                    }
                    _sellerRepo.CreateProduct(product);
                    return RedirectToAction(nameof(Index));
                }
                return View(product);
            }
            catch
            {
                return View();
            }
        }

        // GET: SellerController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SellerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SellerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SellerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}


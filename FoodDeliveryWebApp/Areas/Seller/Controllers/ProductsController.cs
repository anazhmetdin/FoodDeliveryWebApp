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
using System;
using System.Data;

namespace FoodDeliveryWebApp.Areas.Seller.Controllers
{
    [Authorize(Roles = "Seller")]
    [Area("Seller")]
    [AutoValidateAntiforgeryToken]
    public class ProductsController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ISellerRepo _sellerRepo;
        private readonly IModelRepo<Category> _categryRepo;
        private readonly ModelRepo<Product> _productRepo;

        public ProductsController(ISellerRepo sellerRepo, UserManager<AppUser> userManager, ModelRepo<Product> productRepo, IModelRepo<Category> categryRepo)
        {
            _sellerRepo = sellerRepo;
            _userManager = userManager;
            _productRepo = productRepo;
            _categryRepo = categryRepo;
        }

        // GET: Seller/Products
        public ActionResult Index(int? category, string? hasSale)
        {
            ViewBag.hasSale = new SelectList(new List<string>() { "All", "Yes", "No" }, hasSale);
            
            ViewBag.CategoryList = new SelectList(_categryRepo.GetAll(), "Id", "Name", category);
            var sellerId = _userManager.GetUserId(User);

            bool sale = hasSale?.ToLower() == "yes";
            hasSale = hasSale?.ToLower() == "all" ? null : hasSale;

            category = category == 0 ? null : category;

            var products = _sellerRepo.GetSellerProducts(sellerId)
                .Where(s => (s.CategoryId == category || category == null) 
                    && (s.HasSale == sale || hasSale == null) ).ToList();
            
            return View(products);
        }

        // POST: Seller/Products
        [HttpPost]
        public ActionResult Restock(IFormCollection pairs, string? returnUrl)
        {
            var sellerId = _userManager.GetUserId(User);

            _sellerRepo.Restock(pairs, sellerId, true);

            return RedirectToIndex(returnUrl);
        }

        // POST: Seller/Products
        [HttpPost]
        public ActionResult ApplySale(IFormCollection pairs, string? returnUrl)
        {
            var sellerId = _userManager.GetUserId(User);

            _sellerRepo.ApplySale(pairs, sellerId);

            return RedirectToIndex(returnUrl);
        }

        // POST: Seller/Products
        [HttpPost]
        public ActionResult Destock(IFormCollection pairs, string? returnUrl)
        {
            var sellerId = _userManager.GetUserId(User);

            _sellerRepo.Restock(pairs, sellerId, false);

            return RedirectToIndex(returnUrl);
        }

        // GET: Seller/Details/5
        public ActionResult Details(int id)
        {
            var sellerId = _userManager.GetUserId(User);
            ViewBag.sell = sellerId;

            var Model = _sellerRepo.GetSellerProduct(id, sellerId);
            
            if (Model == null)
                return NotFound();
            
            return View(Model);
        }

        // GET: SellerController/Create
        public ActionResult Create()
        {
            var sellerId = _userManager.GetUserId(User);
            ViewBag.sell = sellerId;
            ViewBag.CategoryList = new SelectList(_categryRepo.GetAll(), "Id", "Name");
            return View();
        }

        // POST: SellerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Name,Description,Price,InStock,Sale,SellerId,CategoryId")] Product product, IFormFile Image)
        {
            var sellerId = _userManager.GetUserId(User);
            ViewBag.sell = sellerId;
            ViewBag.CategoryList = new SelectList(_categryRepo.GetAll(), "Id", "Name", product.CategoryId);
            try
            {
                if (ModelState.IsValid)
                {
                    if (product.SellerId == sellerId && _productRepo.TryInsert(product, Image))
                        return RedirectToAction(nameof(Index));
                }
                return View(product);
            }
            catch
            {
                return View();
            }
        }

        // GET: Seller/Edit/5
        public ActionResult Edit(int id)
        {
            var sellerId = _userManager.GetUserId(User);
            ViewBag.sell = sellerId;

            var Model = _sellerRepo.GetSellerProduct(id, sellerId);
            if (Model == null)
            {
                return NotFound();
            }


            ViewBag.CategoryList = new SelectList(_categryRepo.GetAll(),
                "Id", "Name", Model.CategoryId);
                
            return View(Model);
        }

        // POST: SellerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Name,Description,Price,InStock,SellerId,Sale,CategoryId")] Product product, IFormFile? Image)
        {
            try
            {
                var sellerId = _userManager.GetUserId(User);
                ViewBag.sell = sellerId;

                var Model = _sellerRepo.GetSellerProduct(id, sellerId);
                if (Model == null)
                {
                    return NotFound();
                }

                product.Id = id;

                if (Image == null)
                {
                    ModelState.Remove("Image");
                    product.Image = Model.Image;
                }

                if (ModelState.IsValid)
                {
                    if (_productRepo.TryUpdate(product, Image))
                        return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
            }
            
            return View(product);
        }

        // GET: SellerController/Delete/5
        public ActionResult Delete(int id)
        {
            var sellerId = _userManager.GetUserId(User);
            ViewBag.sell = sellerId;

            var Model = _sellerRepo.GetSellerProduct(id, sellerId);
            if (Model == null)
            {
                return NotFound();
            }

            return View(Model);
        }

        // POST: SellerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var sellerId = _userManager.GetUserId(User);
                ViewBag.sell = sellerId;

                var Model = _sellerRepo.GetSellerProduct(id, sellerId);
                if (Model == null)
                {
                    return NotFound();
                }

                if (_productRepo.TryDelete(id))
                    return RedirectToAction(nameof(Index));

                return View(Model);
            }
            catch
            {
                return View();
            }
        }
        ActionResult RedirectToIndex(string? returnUrl)
        {
            if (returnUrl != null)
                return Redirect(returnUrl);
            else
                return RedirectToAction(nameof(Index));
        }
    }
}


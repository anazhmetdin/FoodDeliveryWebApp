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
    public class ProductViewModel : Product
    {
        public new byte[]? Image { get; set; }
    }

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
        public ActionResult Index(int? category, string? hasSale, string? inStock)
        {
            ViewBag.hasSale = new SelectList(new List<string>() { "All", "Yes", "No" }, hasSale);
            ViewBag.inStock = new SelectList(new List<string>() { "All", "In Stock", "Out of Stock" }, inStock);

            var sellerId = _userManager.GetUserId(User);

            bool sale = hasSale?.ToLower() == "yes";
            hasSale = hasSale?.ToLower() == "all" ? null : hasSale;

            bool stock = inStock?.ToLower() == "in stock";
            inStock = inStock?.ToLower() == "all" ? null : inStock;

            category = category == 0 ? null : category;

            var products = _sellerRepo.GetSellerProducts(sellerId)
                .Where(s => (s.CategoryId == category || category == null)
                    && (s.HasSale == sale || hasSale == null)
                    && (s.InStock == stock || inStock == null)).ToList();

            var productsGroups = products
                .OrderByDescending(o => o.InStock)
                .ThenByDescending(o => o.HasSale)
                .ThenByDescending(o => o.Sale)
                .GroupBy(o => o.Category!.Name)
                .OrderBy(g => g.Key);

            ViewBag.CategoryList = new SelectList(_categryRepo.GetAll(), "Id", "Name", category);

            return View(productsGroups);
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

            if (_sellerRepo.TryAddProduct(sellerId, product, Image))
                return RedirectToAction(nameof(Index));
            else
                return View(product);
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
        public ActionResult Edit(int id, [Bind("Name,Description,Price,InStock,SellerId,Sale,CategoryId")] ProductViewModel product, IFormFile? Image)
        {

            var sellerId = _userManager.GetUserId(User);
            ViewBag.sell = sellerId;

            product.Id = id;

            if (_sellerRepo.TryUpdateProduct(sellerId, product, Image))
                return RedirectToAction(nameof(Index));
            else
                return View(product);
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


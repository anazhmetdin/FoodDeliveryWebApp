﻿using FoodDeliveryWebApp.Areas.Identity.Data;
using FoodDeliveryWebApp.Contracts;
using FoodDeliveryWebApp.Models;
using FoodDeliveryWebApp.Models.Categories;
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
    [AutoValidateAntiforgeryToken]
    public class ProductsController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ISellerRepo _sellerRepo;
        private readonly IModelRepo<Category> _categryRepo;
        private readonly ModelRepo<Product> _productRepo;

        public ProductsController(ISellerRepo sellerRepo, UserManager<AppUser> userManager, IModelRepo<Category> categryRepo, ModelRepo<Product> productRepo)
        {
            _sellerRepo = sellerRepo;
            _userManager = userManager;
            _categryRepo = categryRepo;
            _productRepo = productRepo;
        }

        // GET: Seller/Products
        public ActionResult Index()
        {
            ViewBag.CategoryList = new SelectList(_categryRepo.GetAll(), "Id", "Name");
            var sellerId = _userManager.GetUserId(User);

            return View(_sellerRepo.GetSellerProducts(sellerId));
        }

        // POST: Seller/Products
        [HttpPost]
        public ActionResult Restock(IFormCollection pairs)
        {
            var sellerId = _userManager.GetUserId(User);

            _sellerRepo.Restock(pairs, sellerId, true);

            return RedirectToAction(nameof(Index));
        }

        // POST: Seller/Products
        [HttpPost]
        public ActionResult ApplySale(IFormCollection pairs)
        {
            var sellerId = _userManager.GetUserId(User);

            _sellerRepo.ApplySale(pairs, sellerId);

            return RedirectToAction(nameof(Index));
        }

        // POST: Seller/Products
        [HttpPost]
        public ActionResult Destock(IFormCollection pairs)
        {
            var sellerId = _userManager.GetUserId(User);

            _sellerRepo.Restock(pairs, sellerId, false);

            return RedirectToAction(nameof(Index));
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
        public ActionResult Create([Bind("Name,Description,Price,InStock,SellerId,CategoryId")] Product product, IFormFile Image)
        {
            var sellerId = _userManager.GetUserId(User);
            ViewBag.sell = sellerId;
            ViewBag.CategoryList = new SelectList(_categryRepo.GetAll(), "Id", "Name");
            try
            {
                if (ModelState.IsValid)
                {
                    if (_productRepo.TryInsert(product, Image))
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
        public ActionResult Edit(int id, [Bind("Name,Description,Price,InStock,SellerId,CategoryId")] Product product, IFormFile? Image)
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
                ViewBag.CategoryList = new SelectList(_categryRepo.GetAll(), "Id", "Name", Model.CategoryId);

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
    }
}

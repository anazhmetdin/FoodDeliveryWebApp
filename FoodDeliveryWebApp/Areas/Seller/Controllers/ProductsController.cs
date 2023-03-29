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
        private readonly ISellerRepo _sellerRepo;
        private readonly UserManager<AppUser> _userManager;
        private readonly ModelRepo<Product> _productRepo;

        public ProductsController(ISellerRepo sellerRepo, UserManager<AppUser> userManager, ModelRepo<Product> productRepo)
        {
            _sellerRepo = sellerRepo;
            _userManager = userManager;
            _productRepo = productRepo;
        }

        // GET: Seller/Products
        public ActionResult Index()
        {
            var sellerId = _userManager.GetUserId(User);

            return View(_sellerRepo.GetSellerProducts(sellerId));
        }

        // GET: Seller/Details/5
        public ActionResult Details(int id)
        {
            var sellerId = _userManager.GetUserId(User);
            ViewBag.sell = sellerId;
            var Model = _productRepo.GetById(id);
            
            if (Model.SellerId != sellerId)
                return NotFound();
            
            return View(Model);
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
        public ActionResult Create([Bind("Name,Description,Price,InStock,Image,SellerId,CategoryId")] Product product, IFormFile Image)
        {
            var sellerId = _userManager.GetUserId(User);
            ViewBag.sell = sellerId;
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

            var Model = _productRepo.GetById(id);
            if (Model == null || Model.SellerId != sellerId)
            {
                return NotFound();
            }

            return View(Model);
        }

        // POST: SellerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Name,Description,Price,InStock,Image,SellerId,CategoryId")] Product product, IFormFile Image)
        {
            try
            {
                var sellerId = _userManager.GetUserId(User);
                ViewBag.sell = sellerId;

                var Model = _productRepo.GetById(id);
                if (Model == null || Model.SellerId != sellerId)
                {
                    return NotFound();
                }

                product.Id = id;

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

            var Model = _productRepo.GetById(id);
            if (Model == null || Model.SellerId != sellerId)
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

                var Model = _productRepo.GetById(id);
                if (Model == null || Model.SellerId != sellerId)
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


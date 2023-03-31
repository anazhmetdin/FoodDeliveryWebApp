using FoodDeliveryWebApp.Areas.Identity.Data;
using FoodDeliveryWebApp.Contracts;
using FoodDeliveryWebApp.Data;
using FoodDeliveryWebApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using FoodDeliveryWebApp.Models.Enums;

namespace FoodDeliveryWebApp.Repositories
{
    public class SellerRepo : ModelRepo<Seller>, ISellerRepo
    {
        private readonly ModelRepo<Product> _productRepo;
        private readonly UserManager<AppUser> _userManager;
        private readonly IModelRepo<Review> _reviewRepo;
        private readonly IModelRepo<Order> _orderRepo;

        public SellerRepo(FoodDeliveryWebAppContext context,
            ModelRepo<Product> productRepo,
            UserManager<AppUser> userManager,
            IModelRepo<Review> reviewRepo,
            IModelRepo<Order> orderRepo) : base(context)
        {
            _productRepo = productRepo;
            _userManager = userManager;
            _reviewRepo = reviewRepo;
            _orderRepo = orderRepo;

            _productRepo.Query = _productRepo.Query.Include(p => p.Category);
        }

        public ICollection<Product> GetSellerProducts(string? sellerId)
        {
            if (sellerId == null ) { return new List<Product>(); }

            var products = _productRepo.Where(p => p.SellerId == sellerId);

            return products;
        }
        public Product? GetSellerProduct(int pid, string? sellerId)
        {
            if (sellerId == null) { return null; }

            var products = _productRepo.Where(p => p.Id == pid && p.SellerId == sellerId);

            return products.FirstOrDefault();
        }

        public void CreateProduct(Product product)
        {
            Context.Products.Add(product);

            Context.SaveChanges();
        }


        public ICollection<Category> GetSellerCategories(string sellerId)
        {
            var seller = Context.Sellers.Where( s => s.Id == sellerId)
                .Include(s => s.Categories)
                .FirstOrDefault();
            return seller.Categories.ToList();
        }



        public void Restock(IFormCollection pairs, string? sellerId, bool stock)
        {
            if (sellerId == null) { return; }

            if (pairs["selected"].Count > 0)
            {
                foreach (var item in pairs["selected"])
                {
                    if (Int32.TryParse(item, out int id))
                    {
                        Restock(id, sellerId, stock);
                    }
                }

                Context.SaveChanges();
            }
        }

        public void Restock(int id, string? sellerId, bool stock)
        {
            if (sellerId == null) { return; }

            Product? product = GetSellerProduct(id, sellerId);

            if (product != null)
            {
                product.InStock = stock;
            }
        }

        public void ApplySale(IFormCollection pairs, string? sellerId)
        {
            if (sellerId == null) { return; }

            if (Int32.TryParse(pairs["sale"], out int sale))
            {
                if (pairs["selected"].Count > 0)
                {
                    foreach (var item in pairs["selected"])
                    {
                        if (Int32.TryParse(item, out int id))
                        {
                            ApplySale(id, sellerId, sale);
                        }
                    }

                    Context.SaveChanges();
                }
            }

        }

        public void ApplySale(int id, string sellerId, int sale)
        {
            if (sellerId == null) { return; }

            Product? product = GetSellerProduct(id, sellerId);

            if (product != null)
            {
                product.HasSale = sale != 0;
                product.Sale = sale;
            }
        }

        public ICollection<Review> GetReviews(string? sellerId)
        {
            if (sellerId == null) { return new List<Review>(); }

            var reviews = _reviewRepo.Where(p => p.SellerId == sellerId);

            return reviews;
        }

        public List<Order> GetOrders(string? sellerId, OrderStatus orderStatus)
        {
            if (sellerId == null) { return new List<Order>(); }

            var orders = _orderRepo.Where(p => p.SellerId == sellerId
                                          && p.Status == orderStatus);

            return orders;
        }

        public Order? GetOrder(int? id, string? sellerId)
        {
            if (sellerId == null || id == null) { return null; }

            var order = _orderRepo.GetById(id);

            if (order == null || order.SellerId != sellerId)
                order = null;

            return order;
        }

        public bool ChangeOrderStatus(int? id, string? sellerId, OrderStatus? status)
        {
            if (sellerId == null || id == null || status == null) { return false; }

            var order = _orderRepo.GetById(id);

            if (order == null || order.SellerId != sellerId)
                return false;

            order.Status = (OrderStatus)status;
            return Context.SaveChanges() > 0;
        }
    }
}

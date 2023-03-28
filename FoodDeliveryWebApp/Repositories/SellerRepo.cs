using FoodDeliveryWebApp.Areas.Identity.Data;
using FoodDeliveryWebApp.Contracts;
using FoodDeliveryWebApp.Data;
using FoodDeliveryWebApp.Models;
using Microsoft.AspNetCore.Identity;

namespace FoodDeliveryWebApp.Repositories
{
    public class SellerRepo : ModelRepo<Seller>, ISellerRepo
    {
        private readonly ModelRepo<Product> _productRepo;
        private readonly UserManager<AppUser> _userManager;

        public SellerRepo(FoodDeliveryWebAppContext context,
            ModelRepo<Product> productRepo,
            UserManager<AppUser> userManager) : base(context)
        {
            _productRepo = productRepo;
            _userManager = userManager;
        }

        public ICollection<Product> GetSellerProducts(string? sid)
        {
            if (sid == null ) { return new List<Product>(); }

            var products = _productRepo.Where(p => p.SellerId == sid);

            return products;
        }
        public Product? GetSellerProduct(int pid, string? sid)
        {
            if (sid == null) { return null; }

            var products = _productRepo.Where(p => p.Id == pid && p.SellerId == sid);

            return products.FirstOrDefault();
        }

        public void CreateProduct(Product product)
        {
            Context.Products.Add(product);

            Context.SaveChanges();
        }

        public void Restock(IFormCollection pairs, string? sid, bool stock)
        {
            if (sid == null) { return; }

            if (pairs["selected"].Count > 0)
            {
                foreach (var item in pairs["selected"])
                {
                    if (Int32.TryParse(item, out int id))
                    {
                        Restock(id, sid, stock);
                    }
                }

                Context.SaveChanges();
            }
        }

        public void Restock(int id, string? sid, bool stock)
        {
            if (sid == null) { return; }

            Product? product = GetSellerProduct(id, sid);

            if (product != null)
            {
                product.InStock = stock;
            }
        }
    }
}

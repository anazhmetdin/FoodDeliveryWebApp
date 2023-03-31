using FoodDeliveryWebApp.Areas.Identity.Data;
using FoodDeliveryWebApp.Contracts;
using FoodDeliveryWebApp.Data;
using FoodDeliveryWebApp.Models;
using FoodDeliveryWebApp.ViewModels;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using System.Linq;

namespace FoodDeliveryWebApp.Repositories
{
    public class CustomerRestaurantsRepo : ICustomerRestaurantsRepo
    {
        private readonly FoodDeliveryWebAppContext _context;

        public CustomerRestaurantsRepo(FoodDeliveryWebAppContext context)
        {
            _context = context;
        }

        public ICollection<ProductViewModel> GetSellerProducts(string sellerId)
        {
            if (string.IsNullOrWhiteSpace(sellerId))
            {
                throw new ArgumentNullException(nameof(sellerId));
            }

            return _context.Products.Where(p => p.SellerId == sellerId && p.InStock)
                .Select(p => new ProductViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.HasSale? p.SalePrice : p.Price,
                    Image = $"data:image/png;base64,{Convert.ToBase64String(p.Image)}"
                }).ToList();
        }

        public ICollection<SellerViewModel> GetSellers()
        {
            var roleId = _context.Roles.Where(r => r.Name == "Seller").Select(s => s.Id).FirstOrDefault();

            var sellers = _context.Sellers.Include(s => s.Categories).Include(s => s.Reviews)
                .Select(s => new
                {
                    s.Id,
                    s.Logo,
                    Categories = string.Join(", ", s.Categories.Select(sc => sc.Name)),
                    s.StoreName,
                    Rate = s.Reviews.Count == 0? 0 : s.Reviews.Average(r => r.Rate)
                }).ToList();


            List<SellerViewModel> restaurants = new();

            foreach (var seller in sellers)
            {
                restaurants.Add(new()
                {
                    Id = seller.Id,
                    Categories = string.Join(", ", seller.Categories),
                    StoreName = seller.StoreName,
                    Logo = $"data:image/png;base64,{Convert.ToBase64String(seller.Logo)}",
                    Rate = (int)seller.Rate
                });
            }

            return restaurants;
        }

        public ICollection<SellerViewModel> GetSellersFiltered(List<Category> categories, bool hasPromo, bool orderAlpha, bool orderRate)
        {
            var roleId = _context.Roles.Where(r => r.Name == "Seller").Select(s => s.Id).FirstOrDefault();
            var sellers = _context.Sellers.Include(s => s.Categories).Include(s => s.Reviews).AsEnumerable();
            
            if (categories.Count > 0)
                sellers = sellers.Where(s => s.Categories.Any(cat => categories.Contains(cat)));
            
            if (hasPromo)
            {
                var promosCats = _context.PromoCodes.Include(p => p.AppliedTo).SelectMany(p => p.AppliedTo);
                sellers = sellers.Where(s => s.Categories.Any(c => promosCats.Contains(c)));
            }

            if (orderAlpha)
                sellers = sellers.OrderBy(s => s.StoreName);

            if (orderRate)
                sellers = sellers.OrderByDescending(s => s.Reviews.Count == 0 ? 0 : s.Reviews.Average(r => r.Rate));

            var filtered = sellers.Select(s => new
             {
                 s.Id,
                 s.Logo,
                 Categories = string.Join(", ", s.Categories.Select(sc => sc.Name)),
                 s.StoreName,
                 Rate = s.Reviews.Count == 0 ? 0 : s.Reviews.Average(r => r.Rate)
             });

            List<SellerViewModel> restaurants = new();

            foreach (var seller in filtered)
            {
                restaurants.Add(new()
                {
                    Id = seller.Id,
                    Categories = string.Join(", ", seller.Categories),
                    StoreName = seller.StoreName,
                    Logo = $"data:image/png;base64,{Convert.ToBase64String(seller.Logo)}",
                    Rate = (int)seller.Rate
                });
            }

            return restaurants;
        }

        public ICollection<SellerViewModel> GetSellersSearched(string text)
        {
            return _context.Sellers.Include(s => s.Categories).Include(s => s.Reviews)
                .Where(s => s.StoreName.Contains(text))
                       .OrderBy(s => s.StoreName.IndexOf(text))
                       .Select(s => new SellerViewModel()
                       {
                           Id = s.Id,
                           Categories = string.Join(", ", s.Categories.Select(c => c.Name)),
                           StoreName = s.StoreName,
                           Logo = $"data:image/png;base64,{Convert.ToBase64String(s.Logo)}",
                           Rate = s.Reviews.Count == 0 ? 0 : (int)s.Reviews.Average(r => r.Rate)
                       }).ToList();
        }
    }
}
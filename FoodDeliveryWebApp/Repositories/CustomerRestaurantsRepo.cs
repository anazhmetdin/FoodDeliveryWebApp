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
        public Order CreateOrder(string sellerId, string customerId)
        {
            Order order = new Order()
            {
                TotalPrice = 0,
                DeliveryDate = DateTime.Now,
                CheckOutDate = DateTime.Now,
                SellerId = sellerId,
                CustomerId = customerId,
            };
            _context.Orders.Load();
            _context.Orders.Add(order);
            //Console.WriteLine("This the empty CustomerId: " + _context.Orders.ToList().LastOrDefault()?.CustomerId ?? "HMM");
            _context.SaveChanges();// why you do this to me ?
            return _context.Orders.ToList().LastOrDefault() ?? new Order();
        }
        
        public bool UpdateOrder(Order o)
        {
            try
            {
                _context.Update(o);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

        }

        public OrderProduct CreateOrderProduct(int orderId, int prodId)
        {
            OrderProduct orderProduct = new OrderProduct()
            { OrderId = orderId, ProductId = prodId };
            _context.OrderProducts.Add(orderProduct);
            _context.SaveChanges();
            return orderProduct;
        }
        
        public string GetProductSellerID(int productId)
        {
            //new ProductViewModel() { Id = p.Id, Name = p.Name, Price = p.Price, Description = p.Description, Image = p.Image };

            return _context.Products.FirstOrDefault(p => p.Id == productId)?.SellerId ?? string.Empty;
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
                    Price = p.Price,
                    Image = $"data:image/png;base64,{Convert.ToBase64String(p.Image)}"
                }).ToList();
        }

        public ICollection<SellerViewModel> GetSellers()
        {
            var roleId = _context.Roles.Where(r => r.Name == "Seller").Select(s => s.Id).FirstOrDefault();

            var sellers = _context.Sellers.Include(s => s.Categories)
                .Select(s => new
                {
                    s.Id,
                    s.Logo,
                    Categories = string.Join(", ", s.Categories.Select(sc => sc.Name)),
                    s.StoreName
                });


            List<SellerViewModel> restaurants = new();

            foreach (var seller in sellers)
            {
                restaurants.Add(new()
                {
                    Id = seller.Id,
                    Categories = string.Join(", ", seller.Categories),
                    StoreName = seller.StoreName,
                    Logo = $"data:image/png;base64,{Convert.ToBase64String(seller.Logo)}"
                });
            }

            return restaurants;
        }

        public ICollection<SellerViewModel> GetSellersFiltered(List<Category> categories)
        {
            var roleId = _context.Roles.Where(r => r.Name == "Seller").Select(s => s.Id).FirstOrDefault();

            var sellers = _context.Sellers.Include(s => s.Categories).Where(s => s.Categories.Any(cat => categories.Contains(cat)))
                .Select(s => new
                {
                    s.Id,
                    s.Logo,
                    Categories = string.Join(", ", s.Categories.Select(sc => sc.Name)),
                    s.StoreName
                });

            List<SellerViewModel> restaurants = new();

            foreach (var seller in sellers)
            {
                restaurants.Add(new()
                {
                    Id = seller.Id,
                    Categories = string.Join(", ", seller.Categories),
                    StoreName = seller.StoreName,
                    Logo = $"data:image/png;base64,{Convert.ToBase64String(seller.Logo)}"
                });
            }

            return restaurants;
        }

        public ICollection<SellerViewModel> GetSellersSearched(string text)
        {
            return _context.Sellers.Include(s => s.Categories).Where(s => s.StoreName.Contains(text))
                       .OrderBy(s => s.StoreName.IndexOf(text)).Select(s => new SellerViewModel()
                       {
                           Id = s.Id,
                           Categories = string.Join(", ", s.Categories.Select(c => c.Name)),
                           StoreName = s.StoreName,
                           Logo = $"data:image/png;base64,{Convert.ToBase64String(s.Logo)}"
                       }).ToList();
        }
    }
}
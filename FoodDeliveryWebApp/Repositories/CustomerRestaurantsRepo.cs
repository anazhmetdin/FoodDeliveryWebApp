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
                Customer = null,
                Seller = null
            };
            _context.Orders.Add(order);
            _context.SaveChanges();
            return order;
        }
        public int GetPaymentIdByStripeId(string stripeId)
        {
            return _context.Payments.FirstOrDefault(p => p.StripeId == stripeId)?.Id ?? -1;
        }

        public Order GetOrder(int orderId)
        {
            return _context.Orders.Include(o => o.OrderProducts).FirstOrDefault(o => o.Id == orderId);
        }
        public Order GetOrderStripeByPaymentId(string stripePaymentId)
        {
            return _context.Orders.Include(o => o.Payment).FirstOrDefault(o => o.Payment.StripeId == stripePaymentId);
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

        public OrderProduct CreateOrderProduct(int orderId, int prodId, int quantity)
        {
            OrderProduct orderProduct = new OrderProduct()
            { OrderId = orderId, ProductId = prodId, Quantity = quantity,Order = null,Product = null };
            _context.OrderProducts.Add(orderProduct);
            _context.SaveChanges();
            return orderProduct;
        }
        public IEnumerable<OrderProduct> GetOrderProduct(int orderId)
        {
            return _context.OrderProducts.Include(op => op.Product).Where(op => op.OrderId == orderId);
        }
        public string GetProductSellerID(int productId)
        {
            //new ProductViewModel() { Id = p.Id, Name = p.Name, Price = p.Price, Description = p.Description, Image = p.Image };

            return _context.Products.FirstOrDefault(p => p.Id == productId)?.SellerId ?? string.Empty;
        }

        public Customer? GetCustomer(string customerId)
        {
            return _context.Customers.Find(customerId);
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
                    Price = p.HasSale ? p.SalePrice : p.Price,
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
                    s.Rate
                }).ToList();


            List<SellerViewModel> restaurants = new();
            
            FileStream fs = new FileStream("wwwroot/images/restaurant.jpg", FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            byte[] imageBytes = br.ReadBytes((int)fs.Length);

            foreach (var seller in sellers)
            {
                restaurants.Add(new()
                {
                    Id = seller.Id,
                    Categories = string.Join(", ", seller.Categories),
                    StoreName = seller.StoreName,
                    Logo = $"data:image/png;base64,{Convert.ToBase64String(seller.Logo?? imageBytes)}",
                    Rate = seller.Rate
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
                var promosCats = _context.PromoCodes.Where(p => p.StartDate <= DateTime.Now && p.EndDate > DateTime.Now)
                    .Include(p => p.AppliedTo).SelectMany(p => p.AppliedTo);
                sellers = sellers.Where(s => s.Categories.Any(c => promosCats.Contains(c)));
            }

            if (orderAlpha)
                sellers = sellers.OrderBy(s => s.StoreName);

            if (orderRate)
                sellers = sellers.OrderByDescending(s => s.Rate);

            var filtered = sellers.Select(s => new
            {
                s.Id,
                s.Logo,
                Categories = string.Join(", ", s.Categories.Select(sc => sc.Name)),
                s.StoreName,
                Rate = s.Rate
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
                    Rate = seller.Rate
                });
            }

            return restaurants;
        }

        public ICollection<SellerViewModel> GetSellersSearched(string text)
        {
            FileStream fs = new FileStream("wwwroot/images/restaurant.jpg", FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            byte[] imageBytes = br.ReadBytes((int)fs.Length);

            return _context.Sellers.Include(s => s.Categories).Include(s => s.Reviews)
                .Where(s => s.StoreName.Contains(text))
                       .OrderBy(s => s.StoreName.IndexOf(text))
                       .Select(s => new SellerViewModel()
                       {
                           Id = s.Id,
                           Categories = string.Join(", ", s.Categories.Select(c => c.Name)),
                           StoreName = s.StoreName,
                           Logo = $"data:image/png;base64,{Convert.ToBase64String(s.Logo?? imageBytes)}",
                           Rate = s.Rate
                       }).ToList();
        }
    }
}
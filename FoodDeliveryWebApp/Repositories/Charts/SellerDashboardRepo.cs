using FoodDeliveryWebApp.Contracts;
using FoodDeliveryWebApp.Contracts.Charts;
using FoodDeliveryWebApp.Models;
using FoodDeliveryWebApp.ViewModels.Charts;

namespace FoodDeliveryWebApp.Repositories.Charts
{
    public class SellerDashboardRepo : ISellerDashboardRepo
    {
        private ISellerRepo _sellerRepo;
        public SellerDashboardRepo(ISellerRepo sellerRepo)
        {
            _sellerRepo = sellerRepo;
        }

        public SellerDashboard GetSellerDashboard(string sellerId, int? year)
        {
            SellerDashboard dashBoard = new();

            dashBoard.Years = _sellerRepo.GetSalesYears(sellerId);

            if (dashBoard.Years == null || dashBoard.Years.Count == 0)
            {
                return dashBoard;
            }

            if (!year.HasValue)
            {
                //if year not exists then set it with the last year from years list.
                year = dashBoard.Years.FirstOrDefault();
            }

            List<Order> SalesPerYear = _sellerRepo.GetSalesPerYear(sellerId, year.Value);

            if (SalesPerYear == null || SalesPerYear.Count == 0)
            {
                return dashBoard;
            }

            dashBoard.SalesPerYear =
                      SalesPerYear.GroupBy(g => g.DeliveryDate.GetValueOrDefault().Month)
                      .Select(g => new LineChartData<DateTime, int>(
                              g.FirstOrDefault()!.DeliveryDate.GetValueOrDefault().Date,
                              g.Sum(o => o.OrderProducts.Sum(op => op.Quantity))
                          )
                      )
                      .ToList();

            dashBoard.IncomePerYear =
                      SalesPerYear.GroupBy(g => g.DeliveryDate.GetValueOrDefault().Month)
                      .Select(g => new LineChartData<DateTime, decimal>(
                          g.FirstOrDefault()!.DeliveryDate.GetValueOrDefault().Date,
                          g.Sum(g => g.TotalPrice))
                      ).ToList();

            var allOrderProducts = SalesPerYear.SelectMany(o => o.OrderProducts);
            var totalCount = allOrderProducts.Sum(op => op.Quantity);

            dashBoard.SalesPerCategory = allOrderProducts
                .GroupBy(g => g.Product.Category!.Name)
                .Select(g => new PieChartData<decimal>(
                    g.Key,
                    g.Sum(op => op.Quantity) * 100 / (decimal)totalCount)
                )
                .OrderByDescending(pc => pc.Value)
                .Take(5)
                .ToList();

            dashBoard.SalesPerProduct = allOrderProducts
                      .GroupBy(g => g.Product.Name)
                      .Select(g => new PieChartData<decimal>(
                          g.Key,
                          g.Sum(op => op.Quantity) * 100 / (decimal)totalCount)
                      )
                      .OrderByDescending(pc => pc.Value)
                      .Take(5)
                      .ToList();

            dashBoard.SalesPerCategory.Last().Value =
                100 - dashBoard.SalesPerCategory.Where(d =>
                {
                    return d != dashBoard.SalesPerCategory.Last();
                }).Sum(d => d.Value);


            dashBoard.SalesPerProduct.Last().Value =
                100 - dashBoard.SalesPerProduct.Where(d =>
                {
                    return d != dashBoard.SalesPerProduct.Last();
                }).Sum(d => d.Value);

            return dashBoard;
        }
    }
}

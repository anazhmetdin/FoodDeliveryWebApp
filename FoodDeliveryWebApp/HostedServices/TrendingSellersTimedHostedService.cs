using FoodDeliveryWebApp.Contracts;
using FoodDeliveryWebApp.Models;

namespace FoodDeliveryWebApp.HostedServices
{
    public class TrendingSellersTimedHostedService : IHostedService, IDisposable
    {
        private int executionCount = 0;
        private readonly ILogger<TrendingSellersTimedHostedService> _logger;
        private readonly IServiceProvider _serviceProvider;
        private Timer? _timer = null;

        public TrendingSellersTimedHostedService(ILogger<TrendingSellersTimedHostedService> logger,
            IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Trending Seller Timed Hosted Service running.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromDays(1));

            return Task.CompletedTask;
        }

        private void DoWork(object? state)
        {
            var count = Interlocked.Increment(ref executionCount);

            _logger.LogInformation(
                "Trending Seller Timed Hosted Service is working. Count: {Count}", count);

            bool result = false;

            using (var scope = _serviceProvider.CreateScope())
            {

                var _trendingSeller = scope.ServiceProvider.GetRequiredService<ITrendingSellerRepo>();
                var _ordersRepo = scope.ServiceProvider.GetRequiredService<IModelRepo<Order>>();

                var lastThreeDays = DateTime.UtcNow.AddDays(-3);

                var sellers = _ordersRepo.Query
                    .Where(o => o.DeliveryDate >= lastThreeDays)
                    .GroupBy(o => o.SellerId)
                    .Where(g => g.Count() > 0)
                    .Select(g => g.Key)
                    .Take(10)
                    .ToList();

                result = _trendingSeller.TryUpdateAll(sellers);
            }


            _logger.LogInformation(
                "Trending Seller Timed Hosted Service Result: {Result}", result);
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}

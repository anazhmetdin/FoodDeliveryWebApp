using FoodDeliveryWebApp.Hubs;
using FoodDeliveryWebApp.Models;
using System.Diagnostics;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.EventArgs;
using ErrorEventArgs = TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs;

namespace FoodDeliveryWebApp.SubscribeTableDependencies
{
    public class SubscribeOrderTableDependency : ISubscribeTableDependency
    {
        public static SqlTableDependency<Order> tableDependency;
        SellerOrdersIndexHub sellerOrdersIndexHub;

        public SubscribeOrderTableDependency(SellerOrdersIndexHub sellerOrdersIndexHub)
        {
            this.sellerOrdersIndexHub = sellerOrdersIndexHub;
        }

        public void SubscribeTableDependency(string connectionString)
        {
            tableDependency = new SqlTableDependency<Order>(connectionString, "Orders", "dbo");
            tableDependency.OnChanged += TableDependency_OnChangedAsync;
            tableDependency.OnError += TableDependency_OnError;
            tableDependency.Start();
        }

        private async void TableDependency_OnChangedAsync(object sender, RecordChangedEventArgs<Order> e)
        {
            if (e.ChangeType != TableDependency.SqlClient.Base.Enums.ChangeType.None)
            {
                await sellerOrdersIndexHub.SendOrders();
            }
        }

        private void TableDependency_OnError(object sender, ErrorEventArgs e)
        {
            Console.WriteLine($"{nameof(Order)} SqlTableDependency error: {e.Error.Message}");
        }
    }
}

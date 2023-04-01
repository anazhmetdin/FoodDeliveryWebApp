namespace FoodDeliveryWebApp.ViewModels.Charts
{
    public class SellerDashboard
    {
        public List<int> Years { get; set; } = new();
        public List<LineChartData<DateTime, int>> SalesPerYear { get; set; } = new();
        public List<LineChartData<DateTime, decimal>> IncomePerYear { get; set; } = new();
        public List<PieChartData<decimal>> SalesPerCategory { get; set; } = new();
        public List<PieChartData<decimal>> SalesPerProduct { get; set; } = new();
    }
}

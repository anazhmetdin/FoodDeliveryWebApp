namespace FoodDeliveryWebApp.ViewModels.Charts
{
    public class LineChartData<T,U>
    {
        public T X { get; set; }
        public U Y { get; set; }

        public LineChartData()
        {
            X = default!;
            Y = default!;
        }

        public LineChartData(T x, U y)
        {
            X = x;
            Y = y;
        }
    }
}

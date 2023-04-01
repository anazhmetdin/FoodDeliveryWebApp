namespace FoodDeliveryWebApp.ViewModels.Charts
{
    public class PieChartData<T>
    {
        public string Label { get; set; }
        public T Value { get; set; }

        public PieChartData()
        {
            Label = string.Empty;
            Value = default!;
        }

        public PieChartData(string label, T value)
        {
            Label = label;
            Value = value;
        }
    }
}

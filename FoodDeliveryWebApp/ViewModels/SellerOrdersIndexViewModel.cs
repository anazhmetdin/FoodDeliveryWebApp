using FoodDeliveryWebApp.Models;
using FoodDeliveryWebApp.Models.Enums;
using System.ComponentModel;

namespace FoodDeliveryWebApp.ViewModels
{
    public class SellerOrderButton
    {
        public string ClassList { get; set; } = "";
        public string Content { get; set; } = "";
        public OrderStatus Status { get; set; }
    }

    public class SellerOrderButtons
    {
        public static SellerOrderButton Accept = new() { ClassList = "btn-success accept", Content = "Accept", Status = OrderStatus.InProgress };
        public static SellerOrderButton Delivered = new() { ClassList = "btn-info delivered", Content = "Delivered", Status = OrderStatus.Delivered };
        public static SellerOrderButton Cancel = new() { ClassList = "btn-danger cancel", Content = "Cancel", Status = OrderStatus.Rejected };
    }

    public class SellerOrdersViewData
    {
        public IEnumerable<Order> Oders { get; set; } = new List<Order>();
        public List<SellerOrderButton> Buttons { get; set; } = new List<SellerOrderButton>();
    }

    public class SellerOrdersIndexViewModel
    {
        [DisplayName("Posted")]
        public SellerOrdersViewData PostedOrders { get; set; } = new SellerOrdersViewData();
        [DisplayName("In Progress")]
        public SellerOrdersViewData InProgressOrders { get; set; } = new SellerOrdersViewData();
    }
}

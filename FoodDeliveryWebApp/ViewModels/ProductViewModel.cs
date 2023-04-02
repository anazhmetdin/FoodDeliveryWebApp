using FoodDeliveryWebApp.Models;
using FoodDeliveryWebApp.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryWebApp.ViewModels
{
    public class ProductViewModel
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required decimal Price { get; set; }
        public required string Image { get; set; }
        public int Quantity { get; set; }
    }
}
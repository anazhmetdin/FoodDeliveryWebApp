using System.ComponentModel.DataAnnotations;

namespace FoodDeliveryWebApp.Areas.Admin.ViewModels
{
    public class RoleFormViewModel
    {
        [Required, StringLength(256)]
        public string Name { get; set; }
    }
}

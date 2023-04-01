using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Web.Http.Controllers;

namespace FoodDeliveryWebApp.Models.Authorization
{
    public class DenySellerAttribute : Attribute, IAuthorizationFilter
    {

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.IsInRole("Seller"))
                context.Result = new RedirectToActionResult("Index", "Products", new { area = "Seller" });
        }
    }
}

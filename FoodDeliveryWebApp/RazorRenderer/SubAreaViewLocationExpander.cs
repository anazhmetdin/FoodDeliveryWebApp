using Microsoft.AspNetCore.Mvc.Razor;

namespace FoodDeliveryWebApp.RazorRenderer
{
    public class SubAreaViewLocationExpander : IViewLocationExpander
    {
        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            string subArea = RazorViewEngine.GetNormalizedRouteValue(context.ActionContext, "Areas");
            IEnumerable<string> subAreaViewLocation = new string[]
            {
                "/Areas/Seller/Views/Shared/{0}.cshtml"
            };

            viewLocations = subAreaViewLocation.Concat(viewLocations);

            return viewLocations;
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
            if (context.ActionContext.ActionDescriptor.RouteValues.TryGetValue("Areas", out string? areas))
                context.Values["Areas"] = areas;
        }
    }
}

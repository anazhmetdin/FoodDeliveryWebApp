namespace FoodDeliveryWebApp.RazorRenderer
{
    public interface IRazorPartialToStringRenderer
    {
        Task<string> RenderPartialToStringAsync<TModel>(string partialName, TModel model, HttpContext httpContext);
    }
}

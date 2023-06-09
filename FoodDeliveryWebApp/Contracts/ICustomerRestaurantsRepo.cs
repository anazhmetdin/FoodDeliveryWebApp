﻿using FoodDeliveryWebApp.Areas.Identity.Data;
using FoodDeliveryWebApp.Models;
using FoodDeliveryWebApp.ViewModels;

namespace FoodDeliveryWebApp.Contracts
{
    public interface ICustomerRestaurantsRepo
    {

        public ICollection<SellerViewModel> GetSellers();
        public string GetProductSellerID(int productId);
        public int GetPaymentIdByStripeId(string stripeId);
        public Order CreateOrder(string sellerId, string customerId);
        public bool UpdateOrder(Order o);
        public Order GetOrder(int orderId);
        public Order GetOrderStripeByPaymentId(string stripePaymentId);



        public OrderProduct CreateOrderProduct(int orderId, int prodId, int quantity);
        public IEnumerable<OrderProduct> GetOrderProduct(int orderId);

        public ICollection<ProductViewModel> GetSellerProducts(string sellerId);

        public ICollection<SellerViewModel> GetSellersFiltered(List<Category> categoriesbool, bool hasPromo, bool orderAlpha, bool orderRate);

        public ICollection<SellerViewModel> GetSellersSearched(string text);

        public Customer? GetCustomer(string customerId);
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    //public class OrderModel
    //{
    //    public string orderId{get;set;}
    //    public long productId {get;set;}
    //    public double quantity {get;set;}
    //    public decimal price {get;set;}
    //    public decimal total {get;set;}
    //    public string userId{get;set;}
    //    public string firstName { get; set; }
    //    public string lastName { get; set; }
    //    public string email { get; set; }
    //    public string phone { get; set; }
    //    public int amount { get; set; }
    //}
    public class OrderModel : Model
    {
        public long productId { get; set; }
        public long OrderId { get; set; }
        public string productOwnerId { get; set; }//userid of the owner of the product
        public string productBuyerId { get; set; }//userid of the person buying the product
        public int quantityBought { get; set; }
        public decimal currentPriceAtPurchase { get; set; } // price of the product at purchase
        public decimal TotalAmountPaid { get; set; }
        public ProductModel product { get; set; }
        public string callbackUrl { get; set; }
        public string reference { get; set; }
        public string OrderOption { get; set; }
        public string deliveryAddress { get; set; }
        public string buyerContact { get; set; }
    }
}

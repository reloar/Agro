using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Entities
{
    public class Order:Entity
    {
        public long productId { get; set; }
        public long OrderId { get; set; }
        public string productOwnerId { get; set; }//userid of the owner of the product
        public string productBuyerId { get; set; }//userid of the person buying the product
        public int quantityBought { get; set; }
        public decimal currentPriceAtPurchase { get; set; } // price of the product at purchase
        public decimal TotalAmountPaid { get; set; }
        public string callbackUrl { get; set; }
        public string reference { get; set; }
        public string deliveryAddress { get; set; }
        public string buyerContact { get; set; }

        //public long ProductId { get; set; }
        //public string CurrentuserId { get; set; }
        //public decimal PricePerUnit { get; set; }
        //public int Quantity { get; set; }
        //public decimal StorePrice { get; set; }
        //public decimal TransactionFee { get; set; }
        //public decimal Total { get; set; }

        //public decimal IsSuccessful { get; set; }

        public OrderOption OrderOption { get; set; }
    }
    public enum OrderOption
    {
        Store,
        Delivery
    }
}

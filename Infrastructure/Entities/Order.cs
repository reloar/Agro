using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Entities
{
    public class Order:Entity
    {
       // public string OrderId { get; set; }
        public long ProductId { get; set; }
        public string CurrentuserId { get; set; }
        public decimal PricePerUnit { get; set; }
        public int Quantity { get; set; }
        public decimal StorePrice { get; set; }
        public decimal TransactionFee { get; set; }
        public decimal Total { get; set; }
        //public decimal AmountPaid { get; set; }
        public decimal IsSuccessful { get; set; }

        public OrderOption OrderOption { get; set; }
    }
    public enum OrderOption
    {
        Store,
        OutrightPurchase
    }
}

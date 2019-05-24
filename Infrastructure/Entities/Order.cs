using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Entities
{
    public class Order:Entity
    {
        public string OrderId { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal IsSuccessful { get; set; }
        public double Quantity { get; set; }
        public decimal Price { get; set; }
        public OrderOption OrderOption { get; set; }
        public decimal Total { get; set; }
    }
    public enum OrderOption
    {
        Store,
        OutrightPurchase
    }
}

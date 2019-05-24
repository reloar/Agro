using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class OrderModel
    {
        public string orderId{get;set;}
        public long productId {get;set;}
        public double quantity {get;set;}
        public decimal price {get;set;}
        public decimal total {get;set;}
        public string userId{get;set;}
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Entities
{
    public class StoreTable : Entity
    {
        public string UserId { get; set; }
        public long productId { get; set; }
        public long Orderid { get; set; }
        public int Quantity { get; set; }
        public decimal PricePerProduct { get; set; }
        public Decision Decision { get; set; }
    }
    public enum Decision
    {
        Initial,
        Sell
    }
}

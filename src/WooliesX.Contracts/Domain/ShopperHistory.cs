using System.Collections.Generic;

namespace WooliesX.Contracts.Domain
{
    public class ShopperHistory
    {
        public int CustomerId { get; set; }

        public List<Product> Products { get; set; }
    }
}
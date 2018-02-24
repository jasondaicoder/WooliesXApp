using System.Collections.Generic;

namespace WooliesX.Contracts.Domain
{
    public class SpecialItem
    {
        public List<ProductItem> Quantities { get; set; }

        public double Total { get; set; }
    }
}
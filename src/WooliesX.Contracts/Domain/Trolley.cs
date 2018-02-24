using System.Collections.Generic;

namespace WooliesX.Contracts.Domain
{
    /// <summary>
    /// Trolley
    /// </summary>
    public class Trolley
    {
        /// <summary>
        /// Product list
        /// </summary>
        /// <returns></returns>
        public List<ProductInfo> Products { get; set; }

        /// <summary>
        /// Special info list
        /// </summary>
        /// <returns></returns>
        public List<SpecialItem> Specials { get; set; }

        /// <summary>
        /// Product quantity list
        /// </summary>
        /// <returns></returns>
        public List<ProductItem> Quantities { get; set; }
    }
}
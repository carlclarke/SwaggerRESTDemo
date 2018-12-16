using System;
using System.Collections.Generic;

namespace SwaggerRESTDemo.Models
{
    /// <summary>
    /// Details of a Product's Special Price
    /// </summary>
    public partial class ProductSpecialPrice
    {
        public Product Product { get; set; }
        public List<SpecialPrice> Prices { get; set; }
    }

    /// <summary>
    /// Details for a Special Product Price
    /// </summary>
    public class SpecialPrice
    {
        /// <summary>
        /// The minimum quantity to be sold to achieve this price
        /// </summary>
        public int Minimum { get; set; }
        /// <summary>
        /// The maximum quantity to be sold to achieve this price
        /// </summary>
        public int Maximum { get; set; }
        /// <summary>
        /// The price
        /// </summary>
        public decimal SalesPrice { get; set; }
    }
}

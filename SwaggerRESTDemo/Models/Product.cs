using System;
using System.Collections.Generic;

namespace SwaggerRESTDemo.Models
{
    /// <summary>
    /// Details of a Product
    /// </summary>
    public partial class Product
    {
        /// <summary>
        /// Product Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Product Description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Product Sales Price for One Unit
        /// </summary>
        public decimal SalesPrice { get; set; }
        /// <summary>
        /// Product Cost Price for One Unit
        /// </summary>
        public decimal CostPrice { get; set; }
    }
}

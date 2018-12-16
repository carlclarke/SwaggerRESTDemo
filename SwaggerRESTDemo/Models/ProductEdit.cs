using System;
using System.Collections.Generic;

namespace SwaggerRESTDemo.Models
{
    /// <summary>
    /// Details of Product used for creating and updating Products
    /// </summary>
    public partial class ProductEdit
    {
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

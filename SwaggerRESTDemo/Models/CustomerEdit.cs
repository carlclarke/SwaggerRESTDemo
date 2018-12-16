using System;
using System.Collections.Generic;

namespace SwaggerRESTDemo.Models
{
    /// <summary>
    /// Details of Customer used for creating and updating Customers
    /// </summary>
    public partial class CustomerEdit
    {
        /// <summary>
        /// Customer Title
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Customer Firstname
        /// </summary>
        public string Firstname { get; set; }
        /// <summary>
        /// Customer Lastname
        /// </summary>
        public string Lastname { get; set; }
        /// <summary>
        /// Customer Date of Birth
        /// </summary>
        public DateTime? DateOfBirth { get; set; }
        /// <summary>
        /// Customer Address
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Customer Country
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// Customer Postcode, Zipcode, etc.
        /// </summary>
        public string PostalCode { get; set; }
    }
}

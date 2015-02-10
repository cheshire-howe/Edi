using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edi.Models.Shared
{
    public class Item
    {
        /// <summary>
        /// Id
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// PO101 - Alphanumeric characters assigend for differentiation
        /// wPOhin a transaction set
        /// </summary>
        [MaxLength(20)]
        public string PO101_AssignedIdentification { get; set; }
        /// <summary>
        /// PO102 - Number of units invoiced (supplier units)
        /// </summary>
        public int? PO102_QuantityOrdered { get; set; }
        /// <summary>
        /// PO103 - Code specifying the units in which a value is expressed
        /// For example "EA" meaning 'each'
        /// </summary>
        [MaxLength(2)]
        public string PO103_UnitOfMeasurement { get; set; }
        /// <summary>
        /// PO104 - Price per unit
        /// </summary>
        public decimal? PO104_UnitPrice { get; set; }
        /// <summary>
        /// PO105 - Code Identifying the type of unit price for an item
        /// This data element is used to clarify or alter the basis of
        /// unit price. The unit price PO104 is expressed in terms of
        /// PO103 unless otherwise specified in this element PO105
        /// </summary>
        [MaxLength(2)]
        public string PO105_BasisOfUnitPriceCode { get; set; }
        /// <summary>
        /// PO106 - Code identifying the type number in PO107
        /// "VP" meaning 'Vendor's Part Number'
        /// </summary>
        [MaxLength(2)]
        public string PO106_ProductIdQualifier { get; set; }
        /// <summary>
        /// PO107 - Identifying number for a product or service (Vendor)
        /// </summary>
        [MaxLength(48)]
        public string PO107_ProductID { get; set; }
        /// <summary>
        /// PO108 - Code identifying the type number in PO107
        /// "BP" meaning 'Buyer's Part Number'
        /// </summary>
        [MaxLength(2)]
        public string PO108_ProductIdQualifier { get; set; }
        /// <summary>
        /// PO109 - Identifying number for a product or service (Buyer)
        /// </summary>
        [MaxLength(48)]
        public string PO109_ProductID { get; set; }
    }
}

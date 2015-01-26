using System.ComponentModel.DataAnnotations;

namespace Edi.Models.InvoiceModels
{
    public class InvoiceItem
    {
        /// <summary>
        /// Database Primary Key
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// IT101 - Alphanumeric characters assigend for differentiation
        /// within a transaction set
        /// </summary>
        [MaxLength(20)]
        public string IT101_AssignedIdentification { get; set; }
        /// <summary>
        /// IT102 - Number of units invoiced (supplier units)
        /// </summary>
        public int? IT102_QuantityInvoiced { get; set; }
        /// <summary>
        /// IT103 - Code specifying the units in which a value is expressed
        /// For example "EA" meaning 'each'
        /// </summary>
        [MaxLength(2)]
        public string IT103_UnitOfMeasurement { get; set; }
        /// <summary>
        /// IT104 - Price per unit
        /// </summary>
        public decimal? IT104_UnitPrice { get; set; }
        /// <summary>
        /// IT105 - Code Identifying the type of unit price for an item
        /// This data element is used to clarify or alter the basis of
        /// unit price. The unit price IT104 is expressed in terms of
        /// IT103 unless otherwise specified in this element IT105
        /// </summary>
        [MaxLength(2)]
        public string IT105_BasisOfUnitPriceCode { get; set; }
        /// <summary>
        /// IT106 - Code identifying the type number in IT107
        /// "VP" meaning 'Vendor's Part Number'
        /// </summary>
        [MaxLength(2)]
        public string IT106_ProductIdQualifier { get; set; }
        /// <summary>
        /// IT107 - Identifying number for a product or service (Vendor)
        /// </summary>
        [MaxLength(48)]
        public string IT107_ProductID { get; set; }
        /// <summary>
        /// IT108 - Code identifying the type number in IT107
        /// "BP" meaning 'Buyer's Part Number'
        /// </summary>
        [MaxLength(2)]
        public string IT108_ProductIdQualifier { get; set; }
        /// <summary>
        /// IT109 - Identifying number for a product or service (Buyer)
        /// </summary>
        [MaxLength(48)]
        public string IT109_ProductID { get; set; }
        /// <summary>
        /// IT106 - Code identifying the type number in IT107
        /// "UP" meaning 'UPC Code'
        /// </summary>
        [MaxLength(2)]
        public string IT110_ProductIdQualifier { get; set; }
        /// <summary>
        /// IT107 - Identifying number for a product or service (UPC)
        /// </summary>
        [MaxLength(48)]
        public string IT111_ProductID { get; set; }
        /// <summary>
        /// PID01 - Code indicating the format of a description
        /// If "X" is used, the structure is mutually defined
        /// If "F" is used, it is free-form
        /// </summary>
        [MaxLength(1)]
        public string PID01_ItemDescriptionType { get; set; }
        /// <summary>
        /// PID05 - Description to clarify the related data elements
        /// </summary>
        [MaxLength(80)]
        public string PID05_ItemDescription { get; set; }

        public int InvoiceID { get; set; }
        public virtual Invoice Invoice { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edi.Models.PurchaseOrderModels
{
    public class PoItem
    {
        public PoItem()
        {
            Names = new List<PoItemName>();
            Dtms = new List<PoItemDtm>();            
        }

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
        /// <summary>
        /// CUR01 - Code identifying an organizational entity, for example - ST
        /// </summary>
        [MaxLength(3)]
        public string CUR01_CurrencyEntityIdentifierCode { get; set; }
        /// <summary>
        /// CUR02 - Currency code
        /// ex USD or CAD
        /// </summary>
        [MaxLength(3)]
        public string CUR02_CurrencyCode { get; set; }

        /// <summary>
        /// REF01 - Code qualifying the Reference Identification
        /// 61 = VAT number
        /// CO = Customer order number
        /// VN = Vendor order number
        /// </summary>
        [MaxLength(3)]
        public string REF01_ReferenceIdentificationQualifier { get; set; }
        /// <summary>
        /// REF02 - Reference information as defined by the particular Transaction Set
        /// or as specified by the Reference Identification Qualifier
        /// </summary>
        [MaxLength(30)]
        public string REF02_ReferenceIdentification { get; set; }
        ///// <summary>
        ///// DTM01 - 004 means purchase order date
        ///// </summary>
        //[MaxLength(3)]
        //public string DTM01_DateTimeQualifier { get; set; }
        ///// <summary>
        ///// DTM02 - Purchase order date
        ///// </summary>
        //public DateTime? DTM02_PODate { get; set; }

        public int PurchaseOrderID { get; set; }
        public virtual PurchaseOrder PurchaseOrder { get; set; }

        public virtual List<PoItemName> Names { get; set; }
        public virtual List<PoItemDtm> Dtms { get; set; }
    }
}

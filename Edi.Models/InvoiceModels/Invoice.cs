using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Edi.Models.InvoiceModels
{
    public class Invoice
    {
        /// <summary>
        /// Database Primary Key
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// The UserID is stored in the partnerships table. The edi is read in, and
        /// the SenderID and ReceiverID must match a record in the database, in which
        /// the UserID is stored.
        /// </summary>
        public string UserID { get; set; }
        /// <summary>
        /// BIG01
        /// </summary>
        public DateTime? BIG01_Date { get; set; }
        /// <summary>
        /// BIG02
        /// </summary>
        [MaxLength(22)]
        public string BIG02_InvoiceNumber { get; set; }
        /// <summary>
        /// BIG03
        /// </summary>
        public DateTime? BIG03_Date { get; set; }
        /// <summary>
        /// BIG04
        /// </summary>
        [MaxLength(22)]
        public string BIG04_PurchaseOrderNumber { get; set; }
        /// <summary>
        /// BIG07 - Usually nothing or DI
        /// </summary>
        [MaxLength(2)]
        public string BIG04_TransactionTypeCode { get; set; }
        /// <summary>
        /// BIG08 - Usually 00 for Original
        /// </summary>
        [MaxLength(2)]
        public string BIG08_TransactionSetPurposeCode { get; set; }
        /// <summary>
        /// CUR01 - Code identifying an organizational entity, for example - SE
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
        /// CUR03 - Optional
        /// </summary>
        [MaxLength(10)]
        public string CUR03_ExchangeRate { get; set; }
        /// <summary>
        /// ITD01 - Code identifying type of payment terms
        /// 14 = Previously agreed upon
        /// </summary>
        [MaxLength(2)]
        public string ITD01_TermsTypeCode { get; set; }
        /// <summary>
        /// ITD02 - Code identifying the beginning of the terms period
        /// 3 = invoice date
        /// </summary>
        [MaxLength(2)]
        public string ITD02_TermsBasisDateCode { get; set; }
        /// <summary>
        /// ITD07 - Number of days until invoice amount is due (discount not applicable)
        /// </summary>
        public int? ITD07_TermsNetDays { get; set; }
        /// <summary>
        /// ITD12 - Free-form description to clarify the related data elements and
        /// their content
        /// </summary>
        [MaxLength(80)]
        public string ITD12_Description { get; set; }
        /// <summary>
        /// DTM01 - 011 means shipped
        /// </summary>
        [MaxLength(3)]
        public string DTM01_DateTimeQualifier { get; set; }
        /// <summary>
        /// DTM02 - Date the items were shipped
        /// </summary>
        public DateTime? DTM02_ShipDate { get; set; }
        /// <summary>
        /// TDS01 - Monetary amount which is the total payable including
        /// discounts
        /// </summary>
        public decimal? TDS01_Amount { get; set; }
        /// <summary>
        /// CTT01 - Number of line items in the transaction
        /// </summary>
        public int? CTT01_TransactionTotals { get; set; }

        public InvoiceEnvelope InvoiceEnvelope { get; set; }

        public virtual ICollection<InvoiceNote> Notes { get; set; }
        public virtual ICollection<InvoiceName> Names { get; set; }
        public virtual ICollection<InvoiceItem> Items { get; set; }
        public virtual ICollection<InvoiceRef> Refs { get; set; }


        /* These properties are important but are included in the
         * Names list
        public string DeliveryID { get; set; }
        public int? EmployeeID { get; set; }
        public string SalesPerson { get; set; }
        public string ShipTo { get; set; }
        public string ShippedVia { get; set; }
        public decimal? ShippingCost { get; set; }
        public string Status { get; set; }
         * 
         */
    }
}

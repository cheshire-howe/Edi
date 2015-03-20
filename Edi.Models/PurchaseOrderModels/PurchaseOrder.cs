using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edi.Models.PurchaseOrderModels
{
    public class PurchaseOrder
    {
        public int ID { get; set; }
        /// <summary>
        /// The UserID is stored in the partnerships table. The edi is read in, and
        /// the SenderID and ReceiverID must match a record in the database, in which
        /// the UserID is stored.
        /// </summary>
        public string UserID { get; set; }
        /// <summary>
        /// BIG08 - Usually 00 for Original
        /// </summary>
        [MaxLength(2)]
        public string BEG01_TransactionSetPurposeCode { get; set; }
        /// <summary>
        /// BEG02 - Usually NE
        /// </summary>
        [MaxLength(2)]
        public string BEG02_PurchaseOrderTypeCode { get; set; }
        /// <summary>
        /// BEG03
        /// </summary>
        [MaxLength(22)]
        public string BEG03_PurchaseOrderNumber { get; set; }
        /// <summary>
        /// BEG04
        /// </summary>        
        public DateTime? BEG05_Date { get; set; }
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
        /// CTT01 - Number of line items in the transaction
        /// </summary>
        public int? CTT01_NumberofLineItems { get; set; }
        /// <summary>
        /// AMT01 - TT means Total Transation Amount
        /// </summary>
        [MaxLength(3)]
        public string AMT01_AmountQualifierCode { get; set; }
        /// <summary>
        /// AMT01 - Monetary amount -total extended values of the line items match this total 
        /// amount
        /// </summary>
        public decimal? AMT02_Amount { get; set; }

        public virtual PoEnvelope PoEnvelope { get; set; }

        public virtual List<PoName> Names { get; set; }
        public virtual List<PoItem> Items { get; set; }
        public virtual List<PoRef> Refs { get; set; }
        public virtual List<PoDtm> Dtms { get; set; }
    }
}

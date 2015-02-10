using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edi.Models.AcknowledgmentModels
{
    public class Acknowledgment
    {
        /// <summary>
        /// Id
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// BAK01 - Code identifying purpose of transaction set
        /// 00 -> Original
        /// </summary>
        [MaxLength(2)]
        public string BAK01_TransactionSetPurposeCode { get; set; }
        /// <summary>
        /// BAK02 - Code specifying the type of acknowledgment
        /// </summary>
        [MaxLength(2)]
        public string BAK02_AcknowledgmentType { get; set; }
        /// <summary>
        /// BAK03 - Identifying number for Purchase Order assigned by the orderer/purchaser
        /// </summary>
        [MaxLength(22)]
        public string BAK03_PurchaseOrderNumber { get; set; }
        /// <summary>
        /// BAK04 - Date expressed as CCYYMMDD
        /// </summary>
        public DateTime? BAK04_Date { get; set; }
        /// <summary>
        /// BAK05 - Number identifying a release against a Purchase Order previuosly
        /// placed by the parties involved in the transaction
        /// </summary>
        [MaxLength(30)]
        public string BAK05_ReleaseNumber { get; set; }
        /// <summary>
        /// BAK07 - Contract number
        /// </summary>
        [MaxLength(30)]
        public string BAK07_ContractNumber { get; set; }
        /// <summary>
        /// Code identifying an organizational entity, a physical location, property
        /// or an individual
        /// SE -> Selling party
        /// </summary>
        [MaxLength(3)]
        public string CUR01_EntityIdentifierCode { get; set; }
        /// <summary>
        /// CUR02 - Code (Standard ISO) for country in whose currency the charges
        /// are specified
        /// </summary>
        [MaxLength(3)]
        public string CUR02_CurrencyCode { get; set; }
        /// <summary>
        /// CTT01 - Total number of line items in the transaction set
        /// </summary>
        public int? CTT01_NumberOfLineItems { get; set; }
        /// <summary>
        /// AMT01 - Code to qualify amount
        /// TT -> Total Transaction Amount
        /// </summary>
        [MaxLength(3)]
        public string AMT01_AmountQualifierCode { get; set; }
        /// <summary>
        /// AMT02 - Monetary amount
        /// </summary>
        public decimal? AMT02_MonetaryAmount { get; set; }

        public int AckEnvelopeID { get; set; }
        public AckEnvelope AckEnvelope { get; set; }

        public ICollection<AckRef> AckRefs { get; set; }
        public ICollection<AckTd5> AckTd5s { get; set; }
        public ICollection<AckName> AckNames { get; set; }
        public ICollection<AckItem> AckItems { get; set; }
    }
}

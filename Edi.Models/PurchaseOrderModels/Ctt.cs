using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edi.Models.PurchaseOrderModels
{
    public class Ctt
    {
        /// <summary>
        /// Database Primary Key
        /// </summary>
        public int ID { get; set; }
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

        public int PurchaseOrderID { get; set; }
        public virtual PurchaseOrder PurchaseOrder { get; set; }
    }
}

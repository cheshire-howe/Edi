using System;
using System.ComponentModel.DataAnnotations;
namespace Edi.Models.Shared
{
    public class Dtm
    {
        // This class will have ID and dtm specific elements
        /// <summary>
        /// ID - Database ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// DTM01 - 004 means purchase order form
        /// </summary>
        [MaxLength(3)]
        public string DTM01_DateTimeQualifier { get; set; }
        /// <summary>
        /// DTM02 - User purchase order date
        /// </summary>
        public DateTime? DTM02_PurchaseOrderDate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edi.Models.Shared;

namespace Edi.Models.AsnModels
{
    public class AsnDtm
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
        public DateTime? DTM02_ShipDate { get; set; }
        /// <summary>
        /// DTM03 - If DTM02 is '011' this is actual ship time
        /// If DTM02 is '017' this element is not used
        /// </summary>
        public string DTM03_Time { get; set; }

        public int AsnID { get; set; }
        public Asn Asn { get; set; }
    }
}

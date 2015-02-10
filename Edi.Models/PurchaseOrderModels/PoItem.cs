using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edi.Models.Shared;

namespace Edi.Models.PurchaseOrderModels
{
    public class PoItem : Item
    {
        public PoItem()
        {
            Names = new List<PoItemName>();
            Dtms = new List<PoItemDtm>();            
        }

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

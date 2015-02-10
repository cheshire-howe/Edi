using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edi.Models.AsnModels
{
    public class AsnHlTwoOrder : AsnHl
    {
        /// <summary>
        /// PRN01 - Identifying number for purchase order assigned by the sender/vendor
        /// </summary>
        [MaxLength(22)]
        public string PRF01_PurchaseOrderNumber { get; set; }
        /// <summary>
        /// PRF02 - Date expressed as CCYYMMDD
        /// </summary>
        public DateTime? PRF02_Date { get; set; }

        public int AsnHlOneShipmentID { get; set; }
        public virtual AsnHlOneShipment AsnHlOneShipment { get; set; }
        public virtual ICollection<AsnHlThreeItem> Items { get; set; }
    }
}

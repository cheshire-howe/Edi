using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edi.Models.AsnModels
{
    public class Asn
    {
        /// <summary>
        /// Id
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// BSN01 - Code of '00' to indicate original document
        /// </summary>
        [MaxLength(2)]
        public string BSN01_TransactionSetPurposeCode { get; set; }
        /// <summary>
        /// BSN02 - A number or code to identify the ASN document to the sender.
        /// May use bill of lading number or pro bill if no other shipment ID exists.
        /// </summary>
        [MaxLength(10)]
        public string BSN02_ShipmentIdentifier { get; set; }
        /// <summary>
        /// BSN03 - Local creation date
        /// </summary>
        public DateTime? BSN03_Date { get; set; }
        /// <summary>
        /// BSN04 - Local creation time
        /// </summary>
        public string BSN04_Time { get; set; }
        /// <summary>
        /// CTT01 - Total number of line items in the transaction set
        /// </summary>
        [MaxLength(6)]
        public string CTT01_NumberOfLineItems { get; set; }
        /// <summary>
        /// CTT02 - Sum of values in the specified data element
        /// </summary>
        public int? CTT02_HashTotal { get; set; }
        /// <summary>
        /// CTT03 - Numeric value of weight
        /// </summary>
        public int? CTT03_Weight { get; set; }
        /// <summary>
        /// CTT04 - Code specifying the units in which a value is being expressed
        /// </summary>
        [MaxLength(2)]
        public string CTT04_UnitOfMeasurement { get; set; }
        /// <summary>
        /// CTT05 - Value of volumetric measure
        /// </summary>
        public int? CTT05_Volume { get; set; }
        /// <summary>
        /// CTT06 - Code specifying the units in which a value is being expressed
        /// </summary>
        [MaxLength(2)]
        public string CTT06_UnitOfMeasurement { get; set; }
        /// <summary>
        /// CTT07 - Free-form description to clarify the related data elements
        /// </summary>
        [MaxLength(80)]
        public string CTT07_Description { get; set; }

        public AsnEnvelope AsnEnvelope { get; set; }

        public virtual ICollection<AsnDtm> AsnDtms { get; set; }
        public virtual AsnHlOneShipment Shipment { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edi.Models.AsnModels
{
    public class AsnHlOneShipment : AsnHl
    {
        /// <summary>
        /// TD101 - Any valid AIAG code
        /// </summary>
        [MaxLength(5)]
        public string TD101_PackagingCode { get; set; }
        /// <summary>
        /// TD102 - Number of bundles/pallets in shipment
        /// </summary>
        [MaxLength(5)]
        public string TD102_LadingQuantity { get; set; }
        /// <summary>
        /// TD501 - 'B' Originating carrier
        /// </summary>
        [MaxLength(1)]
        public string TD501_RoutingSequenceCode { get; set; }
        /// <summary>
        /// TD502 - '02' Standard carrier alphabetic code (SCAC)
        /// </summary>
        [MaxLength(2)]
        public string TD502_IdCodeQualifier { get; set; }
        /// <summary>
        /// TD503 - Carrier's SCAC code
        /// </summary>
        [MaxLength(4)]
        public string TD503_IdCode { get; set; }
        /// <summary>
        /// TD504 - 'A'     Air
        ///         'AC'    Air Charter
        ///         'C'     Consolidation
        ///         'E'     Expedited truck
        ///         'JT'    Just in time
        ///         'LT'    Less than truck load
        ///         'M'     Motor
        ///         'R'     Rail
        ///         'O'     Ocean
        ///         'P'     Private parcel service
        /// </summary>
        [MaxLength(2)]
        public string TD504_TransportationMethodMode { get; set; }
        /// <summary>
        /// TD301 - Any valid AIAG code
        /// </summary>
        [MaxLength(2)]
        public string TD301_EquipmentDescriptionCode { get; set; }
        /// <summary>
        /// TD302 - Alpha part of equipment id
        /// </summary>
        [MaxLength(4)]
        public string TD302_EquipmentInitial { get; set; }
        /// <summary>
        /// TD303 - Numeric part of equipment id
        /// </summary>
        [MaxLength(7)]
        public string TD303_EquipmentNumber { get; set; }

        public int AsnID { get; set; }
        public virtual Asn Asn { get; set; }
        public virtual ICollection<AsnHlOneShipmentRef> Refs { get; set; }
        public virtual ICollection<AsnHlOneShipmentName> Names { get; set; }
        public virtual ICollection<AsnHlTwoOrder> Orders { get; set; }
    }
}

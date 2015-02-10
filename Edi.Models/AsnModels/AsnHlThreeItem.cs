using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edi.Models.AsnModels
{
    public class AsnHlThreeItem : AsnHl
    {
        /// <summary>
        /// LIN01 - Line item reference number
        /// </summary>
        [MaxLength(11)]
        public string LIN01_AssignedIdentification { get; set; }
        /// <summary>
        /// LIN02 - 'BP' Buyer's part number
        /// </summary>
        [MaxLength(2)]
        public string LIN02_IdQualifier { get; set; }
        /// <summary>
        /// LIN03 - Buyer's part number
        /// </summary>
        [MaxLength(30)]
        public string LIN03_Id { get; set; }
        /// <summary>
        /// LIN04 - 'UP' UPC code
        /// </summary>
        [MaxLength(2)]
        public string LIN04_IdQualifier { get; set; }
        /// <summary>
        /// LIN05 - UPC code
        /// </summary>
        [MaxLength(30)]
        public string LIN05_Id { get; set; }
        /// <summary>
        /// LIN04 - 'VP' Vendor part number
        /// </summary>
        [MaxLength(2)]
        public string LIN06_IdQualifier { get; set; }
        /// <summary>
        /// LIN05 - Vendor part number
        /// </summary>
        [MaxLength(30)]
        public string LIN07_Id { get; set; }
        /// <summary>
        /// SN101 - Alphanumeric characters assigned for differentiation within a
        /// transaction set
        /// </summary>
        [MaxLength(20)]
        public string SN101_AssignedIdentification { get; set; }
        /// <summary>
        /// SN102 - Numeric value in manufacturer's shipping units
        /// </summary>
        public int? SN102_NumberOfUnitsShipped { get; set; }
        /// <summary>
        /// SN103 - Code specifying units in which a value is being expressed
        /// 'EA' Each
        /// 'FT' Foor
        /// 'LB' Pound
        /// </summary>
        [MaxLength(2)]
        public string SN103_UnitOfMeasurementCode { get; set; }
        /// <summary>
        /// SN105 - Numeric quantity
        /// </summary>
        public int? SN105_QuantityOrdered { get; set; }
        /// <summary>
        /// SN103 - Code specifying units in which a value is being expressed
        /// 'EA' Each
        /// 'FT' Foor
        /// 'LB' Pound
        /// If SN105 is present this is necessary
        /// </summary>
        [MaxLength(2)]
        public string SN106_UnitOfMeasurementCode { get; set; }
        /// <summary>
        /// PID01 - Code indicating the format of a description
        /// 'F' Free-form
        /// </summary>
        [MaxLength(1)]
        public string PID01_ItemDescriptionType { get; set; }
        /// <summary>
        /// PID05 - Free-form description to clarify the related data elements
        /// </summary>
        [MaxLength(80)]
        public string PID05_Description { get; set; }
        /// <summary>
        /// MEA01 - 'PD' for physical dimensions
        /// </summary>
        [MaxLength(2)]
        public string MEA01_MeasurementReferenceCode { get; set; }
        /// <summary>
        /// MEA02 - 'G' Gross weight or
        /// 'N' Net weight
        /// </summary>
        [MaxLength(1)]
        public string MEA02_MeasurementQualifier { get; set; }
        /// <summary>
        /// MEA03 - Weight, no decimal points allowed
        /// </summary>
        public int? MEA03_MeasurementValue { get; set; }
        /// <summary>
        /// MEA04 - 'KG' Kilograms or
        /// 'LB' Pounds
        /// </summary>
        [MaxLength(2)]
        public string MEA04_MeasurementReferenceCode { get; set; }

        public int AsnHlTwoOrderID { get; set; }
        public AsnHlTwoOrder AsnHlTwoOrder { get; set; }
    }
}

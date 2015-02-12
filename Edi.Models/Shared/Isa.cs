using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edi.Models.Shared
{
    public class Isa : Gs
    {
        [MaxLength(2)]
        public string ISA01_AuthInfoQualifier { get; set; }
        [MaxLength(10)]
        public string ISA02_AuthInfo { get; set; }
        [MaxLength(2)]
        public string ISA03_SecurityInfoQualifier { get; set; }
        [MaxLength(10)]
        public string ISA04_SecurityInfo { get; set; }
        [MaxLength(2)]
        public string ISA05_InterchangeSenderIdQualifier { get; set; }
        [MaxLength(15)]
        public string ISA06_InterchangeSenderId { get; set; }
        [MaxLength(2)]
        public string ISA07_InterchangeReceiverIdQualifier { get; set; }
        [MaxLength(15)]
        public string ISA08_InterchangeReceiverId { get; set; }
        public DateTime? ISA09_Date { get; set; }
        [MaxLength(4)]
        public string ISA10_Time { get; set; }
        [MaxLength(1)]
        public string ISA11_InterchangeControlStandardsIdentifier { get; set; }
        [MaxLength(5)]
        public string ISA12_InterchangeControlVersionNumber { get; set; }
        [MaxLength(9)]
        public string ISA13_InterchangeControlNumber { get; set; }
        [MaxLength(1)]
        public string ISA14_AcknowledgmentRequested { get; set; }
        [MaxLength(1)]
        public string ISA15_UsageIndicator { get; set; }
        [MaxLength(1)]
        public string ISA16_ComponentElementSeparator { get; set; }

        [MaxLength(5)]
        public string IEA01_NumberOfIncludedFunctionalGroups { get; set; }
        [MaxLength(9)]
        public string IEA02_InterchangeControlNumber { get; set; }
    }
}

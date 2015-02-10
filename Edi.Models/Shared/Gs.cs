using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edi.Models.Shared
{
    public class Gs : St
    {
        [MaxLength(2)]
        public string GS01_FunctionalIdentifierCode { get; set; }
        [MaxLength(15)]
        public string GS02_ApplicationSenderCode { get; set; }
        [MaxLength(15)]
        public string GS03_ApplicationReceiverCode { get; set; }
        public DateTime? GS04_Date { get; set; }
        [MaxLength(8)]
        public string GS05_Time { get; set; }
        [MaxLength(9)]
        public string GS06_GroupControlNumber { get; set; }
        [MaxLength(2)]
        public string GS07_ResponsibleAgencyCode { get; set; }
        [MaxLength(12)]
        public string GS08_Version { get; set; }
    }
}

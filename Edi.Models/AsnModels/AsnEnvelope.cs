using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edi.Models.Shared;

namespace Edi.Models.AsnModels
{
    public class AsnEnvelope : Isa
    {
        public int ID { get; set; }
        public int AsnID { get; set; }
        public Asn Asn { get; set; }
    }
}

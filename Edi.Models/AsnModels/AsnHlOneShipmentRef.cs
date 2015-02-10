using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edi.Models.Shared;

namespace Edi.Models.AsnModels
{
    public class AsnHlOneShipmentRef : Ref
    {
        public int AsnHlOneShipmentID { get; set; }
        public virtual AsnHlOneShipment AsnHlOneShipment { get; set; }
    }
}

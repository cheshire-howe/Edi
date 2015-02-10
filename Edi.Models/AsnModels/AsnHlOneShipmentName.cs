using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edi.Models.Shared;

namespace Edi.Models.AsnModels
{
    public class AsnHlOneShipmentName : Name
    {
        public int AsnHlOneShipmentID { get; set; }
        public virtual AsnHlOneShipment AsnHlOneShipment { get; set; }
    }
}

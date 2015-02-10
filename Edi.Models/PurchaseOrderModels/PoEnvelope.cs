using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edi.Models.Shared;

namespace Edi.Models.PurchaseOrderModels
{
    public class PoEnvelope : Isa
    {
        public int ID { get; set; }
        public int PurchaseOrderID { get; set; }
        public PurchaseOrder PurchaseOrder { get; set; }
    }
}

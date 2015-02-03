using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edi.Models.Shared;

namespace Edi.Models.PurchaseOrderModels
{
    public class PoName : Name
    {
        public PoName()
        {
            Refs = new List<PoNameRef>();           
        }
        public int PurchaseOrderID { get; set; }
        public virtual PurchaseOrder PurchaseOrder { get; set; }
        public virtual List<PoNameRef> Refs { get; set; }
    }
}

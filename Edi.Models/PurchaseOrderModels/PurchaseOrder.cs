using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edi.Models.PurchaseOrderModels
{
    public class PurchaseOrder
    {
        public int ID { get; set; }
        public virtual List<PoName> Names { get; set; }
        public virtual List<PoItem> Items { get; set; }
        public virtual List<PoRef> Refs { get; set; }
        public virtual List<PoDtm> Dtms { get; set; }
    }
}

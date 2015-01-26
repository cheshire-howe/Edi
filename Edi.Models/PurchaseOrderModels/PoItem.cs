using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edi.Models.PurchaseOrderModels
{
    public class PoItem
    {
        public int ID { get; set; }
        public int PurchaseOrderID { get; set; }
        public virtual PurchaseOrder PurchaseOrder { get; set; }

        public virtual List<PoItemName> Names { get; set; }
        public virtual List<PoItemDtm> Dtms { get; set; }
    }
}

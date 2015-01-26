using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edi.Models.Shared;

namespace Edi.Models.PurchaseOrderModels
{
    public class PoDtm : Dtm
    {
        public int PurchaseOrderID { get; set; }
        public virtual PurchaseOrder PurchaseOrder { get; set; }
    }
}

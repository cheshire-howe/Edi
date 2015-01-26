using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edi.Models.Shared;

namespace Edi.Models.PurchaseOrderModels
{
    public class PoItemDtm : Dtm
    {
        public int PoItemID { get; set; }
        public virtual PoItem Item { get; set; }
    }
}

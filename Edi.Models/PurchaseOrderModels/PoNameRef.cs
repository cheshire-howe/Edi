using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edi.Models.Shared;

namespace Edi.Models.PurchaseOrderModels
{
    public class PoNameRef : Ref
    {
        public int PoNameID { get; set; }
        public PoName Name { get; set; }
    }
}

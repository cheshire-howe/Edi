using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edi.Models.Shared;

namespace Edi.Models.InvoiceModels
{
    public class InvoiceName : Name
    {
        public int InvoiceID { get; set; }
        public virtual Invoice Invoice { get; set; }
    }
}

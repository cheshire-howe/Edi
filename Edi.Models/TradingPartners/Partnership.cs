using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edi.Models.TradingPartners
{
    public class Partnership
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        public int VendorID { get; set; }

        public string CustomerIDQualifier { get; set; }
        public string CustomerEdiID { get; set; }
        public string VendorIDQualifier { get; set; }
        public string VendorEdiID { get; set; }

        public virtual Vendor Vendor { get; set; }
    }
}

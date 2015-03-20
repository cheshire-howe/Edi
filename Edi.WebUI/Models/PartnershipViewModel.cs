using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edi.WebUI.Models
{
    public class PartnershipViewModel
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string VendorName { get; set; }

        public string CustomerIDQualifier { get; set; }
        public string CustomerEdiID { get; set; }
        public string VendorIDQualifier { get; set; }
        public string VendorEdiID { get; set; }
    }
}

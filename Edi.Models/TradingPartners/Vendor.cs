using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edi.Models.TradingPartners
{
    public class Vendor
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public virtual List<Partnership> Partnerships { get; set; }
    }
}

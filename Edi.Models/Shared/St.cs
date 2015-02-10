using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edi.Models.Shared
{
    public class St
    {
        [MaxLength(3)]
        public string ST01_TransactionSetIdentifierCode { get; set; }
        [MaxLength(9)]
        public string ST02_TransactionSetControlNumber { get; set; }
    }
}

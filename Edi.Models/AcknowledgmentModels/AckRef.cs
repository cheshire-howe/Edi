using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edi.Models.Shared;

namespace Edi.Models.AcknowledgmentModels
{
    public class AckRef : Ref
    {
        public int AcknowledgmentID { get; set; }
        public Acknowledgment Acknowledgment { get; set; }
    }
}

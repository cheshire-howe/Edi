using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edi.Logic.Interfaces;
using Edi.Models.AsnModels;
using OopFactory.X12.Parsing;

namespace Edi.Logic.Concrete
{
    public class AsnLogic : IAsnLogic
    {
        public Asn ConvertAsn(FileStream fs)
        {
            var parser = new X12Parser();
            var interchanges = parser.ParseMultiple(fs);

            var asn = new Asn()
            {

            };

            return asn;
        }
    }
}

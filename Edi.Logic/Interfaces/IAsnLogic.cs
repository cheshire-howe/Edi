using System.Collections.Generic;
using System.IO;
using Edi.Models.AsnModels;
using OopFactory.X12.Parsing.Model;

namespace Edi.Logic.Interfaces
{
    public interface IAsnLogic
    {
        Asn ConvertAsn(List<Interchange> interchanges);
    }
}
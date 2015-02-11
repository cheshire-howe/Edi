using System.IO;
using Edi.Models.AsnModels;

namespace Edi.Logic.Interfaces
{
    public interface IAsnLogic
    {
        Asn ConvertAsn(FileStream fs);
    }
}
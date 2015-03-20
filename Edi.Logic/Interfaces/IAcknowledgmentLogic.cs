using System.Collections.Generic;
using System.IO;
using Edi.Models.AcknowledgmentModels;
using OopFactory.X12.Parsing.Model;

namespace Edi.Logic.Interfaces
{
    public interface IAcknowledgmentLogic
    {
        Acknowledgment ConvertAcknowledgment(List<Interchange> interchanges, string userId);
    }
}
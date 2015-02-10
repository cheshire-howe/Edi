using System.IO;
using Edi.Models.AcknowledgmentModels;

namespace Edi.Logic.Interfaces
{
    public interface IAcknowledgmentLogic
    {
        Acknowledgment ConvertAcknowledgment(FileStream fs);
    }
}
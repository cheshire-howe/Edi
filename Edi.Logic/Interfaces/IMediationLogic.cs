using System.Collections.Generic;
using System.IO;
using OopFactory.X12.Parsing.Model;

namespace Edi.Logic.Interfaces
{
    public interface IMediationLogic
    {
        FileInfo[] ProcessDirectory();
        int FindService(List<Interchange> interchanges);
        List<Interchange> GetInterchanges(string filename);
        void MoveFile(FileInfo fInfo, bool noErrors);
    }
}
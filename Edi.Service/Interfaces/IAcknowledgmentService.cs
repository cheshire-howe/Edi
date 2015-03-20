using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edi.Models.AcknowledgmentModels;
using OopFactory.X12.Parsing.Model;

namespace Edi.Service.Interfaces
{
    public interface IAcknowledgmentService : IEntityService<Acknowledgment>
    {
        Acknowledgment GetById(int id);
        IEnumerable<Acknowledgment> GetByUserId(string id);
        void SaveACKEdiFile(List<Interchange> interchanges, string userId);
    }
}

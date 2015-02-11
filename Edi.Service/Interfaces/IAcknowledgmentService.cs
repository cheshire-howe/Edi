using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edi.Models.AcknowledgmentModels;

namespace Edi.Service.Interfaces
{
    public interface IAcknowledgmentService : IEntityService<Acknowledgment>
    {
        Acknowledgment GetById(int id);
        void SaveACKEdiFile(FileStream fs);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edi.Models.AcknowledgmentModels;

namespace Edi.Dal.Interfaces
{
    public interface IAcknowledgmentRepository : IGenericRepository<Acknowledgment>
    {
        Acknowledgment GetById(int id);
    }
}

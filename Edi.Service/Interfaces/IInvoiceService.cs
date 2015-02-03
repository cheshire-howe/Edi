using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edi.Models.InvoiceModels;

namespace Edi.Service.Interfaces
{
    public interface IInvoiceService : IEntityService<Invoice>
    {
        Invoice GetById(int id);
        void SaveEdiFile(FileStream fs);
        void WriteEdiFile(int id);
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edi.Models.InvoiceModels;
using OopFactory.X12.Parsing.Model;

namespace Edi.Service.Interfaces
{
    public interface IInvoiceService : IEntityService<Invoice>
    {
        Invoice GetById(int id);
        IEnumerable<Invoice> GetByUserId(string id);
        void SaveEdiFile(List<Interchange> interchanges, string userId);
        void WriteEdiFile(int id);
    }
}

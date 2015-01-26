using Edi.Models;
using Edi.Models.InvoiceModels;

namespace Edi.Dal.Interfaces
{
    public interface IInvoiceRepository : IGenericRepository<Invoice>
    {
        Invoice GetById(int id);
    }
}
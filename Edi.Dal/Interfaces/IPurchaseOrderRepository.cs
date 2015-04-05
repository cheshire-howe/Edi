using System.Collections.Generic;
using System.Threading.Tasks;
using Edi.Models.PurchaseOrderModels;

namespace Edi.Dal.Interfaces
{
    public interface IPurchaseOrderRepository : IGenericRepository<PurchaseOrder>
    {
        PurchaseOrder GetById(int id);
        Task<PurchaseOrder> GetByIdAsync(int id);
    }
}
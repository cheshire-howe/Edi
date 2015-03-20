using System.Collections.Generic;
using Edi.Models.PurchaseOrderModels;

namespace Edi.Dal.Interfaces
{
    public interface IPurchaseOrderRepository : IGenericRepository<PurchaseOrder>
    {
        PurchaseOrder GetById(int id);
    }
}
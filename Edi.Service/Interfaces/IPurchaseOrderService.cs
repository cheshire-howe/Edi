using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edi.Models.PurchaseOrderModels;
using System.IO;
using OopFactory.X12.Parsing.Model;

namespace Edi.Service.Interfaces
{
    public interface IPurchaseOrderService : IEntityService<PurchaseOrder>
    {
        PurchaseOrder GetById(int id);
        Task<PurchaseOrder> GetByIdAsync(int id);
        IEnumerable<PurchaseOrder> GetByUserId(string id);
        void SavePOEdiFile(List<Interchange> interchanges);
        void SaveAndSend(PurchaseOrder entity, int customerId);
        void WritePOEdiFile(int id, int customerId);        
    }
}

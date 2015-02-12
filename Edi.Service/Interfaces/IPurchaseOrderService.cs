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
        void SavePOEdiFile(List<Interchange> interchanges);
        void WritePOEdiFile(int id);        
    }
}

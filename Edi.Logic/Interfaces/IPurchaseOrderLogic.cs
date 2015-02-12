using System.Collections.Generic;
using Edi.Models.PurchaseOrderModels;
using System.IO;
using OopFactory.X12.Parsing.Model;

namespace Edi.Logic.Interfaces
{
    public interface IPurchaseOrderLogic
    {        
        void WritePurchaseOrderEdi(PurchaseOrder purchaseOrder);
        PurchaseOrder ConvertPurchaseOrder(List<Interchange> interchanges);
    }
}

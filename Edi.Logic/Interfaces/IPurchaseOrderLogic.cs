using System.Collections.Generic;
using Edi.Models.PurchaseOrderModels;
using System.IO;
using OopFactory.X12.Parsing.Model;

namespace Edi.Logic.Interfaces
{
    public interface IPurchaseOrderLogic
    {
        string WritePurchaseOrderEdi(PurchaseOrder purchaseOrder, int customerId);
        void SendPurchaseOrder(string filename);
        PurchaseOrder ConvertPurchaseOrder(List<Interchange> interchanges);
    }
}

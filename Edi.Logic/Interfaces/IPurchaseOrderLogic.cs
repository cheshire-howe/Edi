using Edi.Models.PurchaseOrderModels;
using System.IO;

namespace Edi.Logic.Interfaces
{
    public interface IPurchaseOrderLogic
    {        
        void WritePurchaseOrderEdi(PurchaseOrder purchaseOrder);
        PurchaseOrder ConvertPurchaseOrder(FileStream fs);
    }
}

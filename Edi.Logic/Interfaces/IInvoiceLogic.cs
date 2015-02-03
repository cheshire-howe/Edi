using System.IO;
using Edi.Models.InvoiceModels;

namespace Edi.Logic.Interfaces
{
    public interface IInvoiceLogic
    {
        void WriteInvoiceEdi(Invoice invoice);
        Invoice ConvertInvoice(FileStream fs);
    }
}
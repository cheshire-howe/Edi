using System.IO;
using Edi.Models.InvoiceModels;

namespace Edi.Logic.Interfaces
{
    public interface IInvoiceLogic
    {
        void WriteInvoiceEdi(int id);
        Invoice ConvertInvoice(FileStream fs);
    }
}
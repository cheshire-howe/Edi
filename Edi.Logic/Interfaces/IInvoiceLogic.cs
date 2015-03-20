using System.Collections.Generic;
using System.IO;
using Edi.Models.InvoiceModels;
using OopFactory.X12.Parsing.Model;

namespace Edi.Logic.Interfaces
{
    public interface IInvoiceLogic
    {
        void WriteInvoiceEdi(Invoice invoice);
        Invoice ConvertInvoice(List<Interchange> interchanges, string userId);
    }
}
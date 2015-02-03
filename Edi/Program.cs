using System;
using System.IO;
using System.Linq;

namespace Edi
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting...");
            Get.Started();

            //Invoice
            var fs = new FileStream(@"..\..\..\Example4_Adobe.txt", FileMode.Open, FileAccess.Read);

            // Invoice Service
            var invoiceService = Get.InvoiceService;

            // Saves an incoming Invoice Edi
            invoiceService.SaveEdiFile(fs);

            // Writes an outgoing Edi to file
            invoiceService.WriteEdiFile(1);

            /////////////////////////////////////////////////////
            // Test to read database
            /////////////////////////////////////////////////////

            var invoices = invoiceService.GetAll();

            foreach (var invoice in invoices)
            {
                invoice.Names.ToList().ForEach(x => Console.WriteLine(x.N102_Name));
            }

            //PurchaseOrder
            var fsPo = new FileStream(@"..\..\..\Example2_Adobe_TLP.txt", FileMode.Open, FileAccess.Read);
            
            // PO Service
            var purchaseOrderService = Get.PurchaseOrderService;

            // Saves an incoming PO Edi
            purchaseOrderService.SavePOEdiFile(fsPo);

            // Writes an outgoing PO Edi to file
            purchaseOrderService.WritePOEdiFile(1);

            /////////////////////////////////////////////////////
            // Test to read database
            /////////////////////////////////////////////////////
            var purchaseOrders = purchaseOrderService.GetAll();

            foreach (var po in purchaseOrders)
            {
                po.Names.ToList().ForEach(x => Console.WriteLine(x.N102_Name));
            }
            ///////////////////////////////////////////////////
            Console.WriteLine(purchaseOrders.ToList().Count);

            //foreach (var po in purchaseOrders)
            //{
            //    po.Items.ToList().ForEach(x => x.Names.ToList().ForEach(y => Console.WriteLine(y.N102_Name)));
            //}
            /////////////////////////////////////////////////////

            Console.WriteLine("Done");
            Console.ReadKey();
        }
    }
}

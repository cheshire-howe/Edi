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

            var fs = new FileStream(@"..\..\..\Example4_Adobe.txt", FileMode.Open, FileAccess.Read);

            // Invoice Service
            var invoiceService = Get.InvoiceService;

            // Saves an incoming Invoice Edi
            invoiceService.SaveEdiFile(fs);

            // Writes an outgoing Edi to file
            invoiceService.WriteEdiFile(1);

            // Purchase Order Service
            var pol = Get.PurchaseOrderService;

            /////////////////////////////////////////////////////
            // Test to read database
            /////////////////////////////////////////////////////
            
            var invoices = invoiceService.GetAll();

            foreach (var invoice in invoices)
            {
                invoice.Names.ToList().ForEach(x => Console.WriteLine(x.N102_Name));
            }

            /////////////////////////////////////////////////////

            var purchaseOrders = pol.GetAll().ToList();

            foreach (var po in purchaseOrders)
            {
                po.Items.ToList().ForEach(x => x.Names.ToList().ForEach(y => Console.WriteLine(y.N102_Name)));
            }
            /////////////////////////////////////////////////////

            Console.WriteLine("Done");
            Console.ReadKey();
        }
    }
}

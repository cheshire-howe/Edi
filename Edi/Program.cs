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

            var invoiceService = Get.InvoiceService;
            invoiceService.SaveEdiFile(fs);
            //invoiceService.WriteInvoiceEdi(1);

            var pol = Get.PurchaseOrderService;
            var pos = pol.GetAll().ToList();
            Console.WriteLine(pos.Count);

            /////////////////////////////////////////////////////
            // Test to read database
            /////////////////////////////////////////////////////
            
            var invoices = invoiceService.GetAll();

            foreach (var invoice in invoices)
            {
                invoice.Names.ToList().ForEach(x => Console.WriteLine(x.N102_Name));
            }

            /////////////////////////////////////////////////////

            pos = pol.GetAll().ToList();

            foreach (var po in pos)
            {
                po.Items.ToList().ForEach(x => x.Names.ToList().ForEach(y => Console.WriteLine(y.N102_Name)));
            }
            /////////////////////////////////////////////////////

            Console.WriteLine("Done");
            Console.ReadKey();
        }
    }
}

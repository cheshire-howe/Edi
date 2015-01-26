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

            //var fs = new FileStream(@"..\..\..\Invoice.txt", FileMode.Open, FileAccess.Read);

            var il = Get.InvoiceLogic;
            //il.SaveInvoice(fs);
            //il.WriteInvoiceEdi(1);

            var pol = Get.PurchaseOrderLogic;
            var pos = pol.GetPurchaseOrders();
            Console.WriteLine(pos.Count);

            /////////////////////////////////////////////////////
            // Test to read database
            /////////////////////////////////////////////////////
            var repo = Get.InvoiceRepository;
            var invoices = repo.GetAll();

            foreach (var invoice in invoices)
            {
                invoice.Names.ToList().ForEach(x => Console.WriteLine(x.N102_Name));
            }

            /////////////////////////////////////////////////////

            var por = Get.PurchaseOrderRepository;
            pos = por.GetAll().ToList();

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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Edi.Models.AcknowledgmentModels;
using Edi.Models.AsnModels;

namespace Edi
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting...");
            Get.Started();

            //Inv();
            //Po();
            //Ack();
            //Asn();

            // Mediation Service            
            try
            {
                var mediationService = Get.MediationService;
                mediationService.ProcessDirectory();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Faild to process" + ex.ToString());
            }

            Console.WriteLine("Done");
            Console.ReadKey();
        }

        private static void Inv()
        {
            //Invoice

            // Invoice Service
            var invoiceService = Get.InvoiceService;

            // Writes an outgoing Edi to file
            invoiceService.WriteEdiFile(1);

            /////////////////////////////////////////////////////
            // Test to read database
            /////////////////////////////////////////////////////

            var invoices = invoiceService.GetAll().ToList();

            foreach (var invoice in invoices)
            {
                invoice.Names.ToList().ForEach(x => Console.WriteLine(x.N102_Name));
            }
        }

        private static void Po()
        {
            //PurchaseOrder
            // PO Service
            var purchaseOrderService = Get.PurchaseOrderService;

            // Writes an outgoing PO Edi to file
            purchaseOrderService.WritePOEdiFile(1);

            /////////////////////////////////////////////////////
            // Test to read database
            /////////////////////////////////////////////////////
            var purchaseOrders = purchaseOrderService.GetAll().ToList();

            foreach (var po in purchaseOrders)
            {
                po.Names.ToList().ForEach(x => Console.WriteLine(x.N102_Name));
            }
            ///////////////////////////////////////////////////
            Console.WriteLine(purchaseOrders.ToList().Count);

            foreach (var po in purchaseOrders)
            {
                po.Items.ToList().ForEach(x => x.Names.ToList().ForEach(y => Console.WriteLine(y.N102_Name)));
            }
            /////////////////////////////////////////////////////
        }

        private static void Ack()
        {
            /////////////////////////////////////////////////////
            // Test to read database
            /////////////////////////////////////////////////////
            var acknowledgmentService = Get.AcknowledgmentService;

            var acks = acknowledgmentService.GetAll();

            acks.ToList().ForEach(x => x.AckTd5s.ToList().ForEach(y => Console.WriteLine(y.TD503_IdentificationCode)));
        }

        private static void Asn()
        {
            /////////////////////////////////////////////////////
            // Test to read database - ASN
            /////////////////////////////////////////////////////

            var asnService = Get.AsnService;

            var b = asnService.GetAll().ToList();

            foreach (var asn in b)
            {
                var c = asn.Shipment.ToList();
                foreach (var asnHlOneShipment in c)
                {
                    var d = asnHlOneShipment.Orders;
                    foreach (var asnHlTwoOrder in d)
                    {
                        Console.WriteLine(asnHlTwoOrder.PRF01_PurchaseOrderNumber);
                    }
                }
            }
        }
    }
}

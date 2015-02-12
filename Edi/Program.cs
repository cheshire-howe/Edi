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
            // Pulled from git
            Console.WriteLine("Starting...");
            Get.Started();

            //Inv();
            //Po();
            //Ack();
            Asn();

            Console.WriteLine("Done");
            Console.ReadKey();
        }

        private static void Inv()
        {
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

            var invoices = invoiceService.GetAll().ToList();

            foreach (var invoice in invoices)
            {
                invoice.Names.ToList().ForEach(x => Console.WriteLine(x.N102_Name));
            }
        }

        private static void Po()
        {
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

            var ack = new Acknowledgment()
            {
                AMT01_AmountQualifierCode = "sd",
                BAK04_Date = new DateTime(2015, 1, 11),
                AMT02_MonetaryAmount = (decimal)10.99,
                BAK01_TransactionSetPurposeCode = "ds",
                BAK02_AcknowledgmentType = "cs",
                BAK03_PurchaseOrderNumber = "cx",
                BAK05_ReleaseNumber = "cx",
                BAK07_ContractNumber = "ax",
                CTT01_NumberOfLineItems = 8,
                CUR01_EntityIdentifierCode = "po",
                CUR02_CurrencyCode = "dq",
                AckItems = new List<AckItem>()
                {
                    new AckItem()
                    {
                        PID01_ItemDescriptionType = "o",
                        PID05_Description = "cs",
                        PO101_AssignedIdentification = "0000101",
                        PO102_QuantityOrdered = 2,
                        PO103_UnitOfMeasurement = "EA",
                        PO104_UnitPrice = (decimal)10.99,
                        PO105_BasisOfUnitPriceCode = "xc",
                        PO106_ProductIdQualifier = "ds",
                        PO107_ProductID = "xc",
                        PO108_ProductIdQualifier = "cx",
                        PO109_ProductID = "xc"
                    }
                },
                AckNames = new List<AckName>()
                {
                    new AckName()
                    {
                        N101_EntityIdentifierCode = "ds",
                        N102_Name = "Josh",
                        N103_IdentificationCodeQualifier = "cx",
                        N104_IdentificationCode = "32"
                    }
                },
                AckRefs = new List<AckRef>()
                {
                    new AckRef()
                    {
                        REF01_ReferenceIdentificationQualifier = "on",
                        REF02_ReferenceIdentification = "32",
                        REF03_Description = "ld"
                    }
                },
                AckTd5s = new List<AckTd5>()
                {
                    new AckTd5()
                    {
                        TD502_IdentificationCodeQualifier = "se",
                        TD503_IdentificationCode = "as"
                    }
                }
            };

            acknowledgmentService.Create(ack);


            var fsACK = new FileStream(@"..\..\..\Example1_Adobe855.txt", FileMode.Open, FileAccess.Read);
            // Saves an incoming Invoice Edi
            acknowledgmentService.SaveACKEdiFile(fsACK);

            var acks = acknowledgmentService.GetAll();

            acks.ToList().ForEach(x => x.AckTd5s.ToList().ForEach(y => Console.WriteLine(y.TD503_IdentificationCode)));
        }

        private static void Asn()
        {
            /////////////////////////////////////////////////////
            // Test to read database - ASN
            /////////////////////////////////////////////////////

            var asnEdi = new FileStream(@"..\..\..\Example1.txt", FileMode.Open, FileAccess.Read);

            var asnService = Get.AsnService;

            //asnService.SaveAsnEdiFile(asnEdi);

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

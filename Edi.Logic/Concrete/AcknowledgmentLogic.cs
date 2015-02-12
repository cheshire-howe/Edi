using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edi.Logic.Interfaces;
using System.IO;
using OopFactory.X12.Parsing;
using Edi.Models.AcknowledgmentModels;
using OopFactory.X12.Parsing.Model;

namespace Edi.Logic.Concrete
{
    public class AcknowledgmentLogic : IAcknowledgmentLogic
    {
        public Acknowledgment ConvertAcknowledgment(List<Interchange> interchanges)
        {
            //var parser = new X12Parser();
            //var interchanges = parser.ParseMultiple(fs);

            // Edi section ISA
            var isa = interchanges[0];
            // Edi section GS
            var gs = isa.FunctionGroups.ToList()[0];
            // Edi section ST
            var st = gs.Transactions[0];
            // Edi section BIG
            var bak = st.Segments.FirstOrDefault(x => x.SegmentId == "BAK");
            // Edi section CUR
            var cur = st.Segments.FirstOrDefault(x => x.SegmentId == "CUR");
            // Edi section CTT
            var ctt = st.Loops.Where(x => x.SegmentId == "CTT").ToList()[0];
            var amt = ctt.Segments.ToList()[0];
            // Edi section N1 - loop
            var n1 = st.Loops.Where(x => x.SegmentId == "N1").ToList();
            // Edi section NTE
            var td5 = st.Segments.Where(x => x.SegmentId == "TD5").ToList();
            // Edi section IT1 - loop
            var po1 = st.Loops.Where(x => x.SegmentId == "PO1").ToList();
            // Edi section REF - (ref keyword is taken so variable name can't follow convention)
            var refack = st.Segments.Where(x => x.SegmentId == "REF").ToList();

            var names = ExtractNames(n1);
            var td5s = ExtractTransitDtl(td5);
            var items = ExtractItems(po1);
            var refs = ExtractRefs(refack);

            var Acknowledgment = new Acknowledgment()
            {
                CustomerID = GetCustomerId(names),
                BAK01_TransactionSetPurposeCode = bak != null ? bak.GetElement(1) : null,
                BAK02_AcknowledgmentType = bak != null ? bak.GetElement(2) : null,
                BAK03_PurchaseOrderNumber = bak != null ? bak.GetElement(3) : null,
                BAK04_Date = bak != null ? bak.GetDate8Element(4) : null,
                BAK05_ReleaseNumber = bak != null ? bak.GetElement(5) : null,
                BAK07_ContractNumber = bak != null ? bak.GetElement(6) : null,
                CUR01_EntityIdentifierCode = cur != null ? cur.GetElement(1) : null,
                CUR02_CurrencyCode = cur != null ? cur.GetElement(2) : null,
                CTT01_NumberOfLineItems = ctt != null ? ctt.GetIntElement(1) : null,
                AMT01_AmountQualifierCode = amt != null ? amt.GetElement(1) : null,
                AMT02_MonetaryAmount = amt != null ? amt.GetDecimalElement(2) : null,
                AckNames = names,
                AckTd5s = td5s,
                AckItems = items,
                AckRefs = refs
            };

            Console.WriteLine(isa.SerializeToX12(true));

            return Acknowledgment;
        }
        private List<AckRef> ExtractRefs(List<Segment> invref)
        {
            var refs = new List<AckRef>();

            foreach (var seg in invref)
            {
                var reference = new AckRef()
                {
                    REF01_ReferenceIdentificationQualifier = seg.GetElement(1),
                    REF02_ReferenceIdentification = seg.GetElement(2),
                    REF03_Description = seg.GetElement(3)
                };

                refs.Add(reference);
            }

            return refs;
        }

        private List<AckTd5> ExtractTransitDtl(List<Segment> td5)
        {
            var td5s = new List<AckTd5>();

            foreach (var seg in td5)
            {
                var note = new AckTd5()
                {
                    TD502_IdentificationCodeQualifier = seg.GetElement(1),
                    TD503_IdentificationCode = seg.GetElement(2)
                };

                td5s.Add(note);
            }

            return td5s;
        }

        private List<AckItem> ExtractItems(List<Loop> po1Loop)
        {
            var items = new List<AckItem>();

            foreach (var po1 in po1Loop)
            {
                var pid = po1.Segments.FirstOrDefault(x => x.SegmentId == "PID");

                var item = new AckItem();

                item.PO101_AssignedIdentification = po1.GetElement(1);
                item.PO102_QuantityOrdered = po1.GetIntElement(2);
                item.PO103_UnitOfMeasurement = po1.GetElement(3);
                item.PO104_UnitPrice = po1.GetDecimalElement(4);
                item.PO105_BasisOfUnitPriceCode = po1.GetElement(5);
                item.PO106_ProductIdQualifier = po1.GetElement(6);
                item.PO107_ProductID = po1.GetElement(7);
                item.PO108_ProductIdQualifier = po1.GetElement(8);
                item.PO109_ProductID = po1.GetElement(9);

                if (pid != null)
                {
                    item.PID01_ItemDescriptionType = pid.GetElement(1);
                    item.PID05_Description = pid.GetElement(5);
                }

                items.Add(item);
            }

            return items;
        }

        private List<AckName> ExtractNames(List<Loop> n1Loop)
        {
            var names = new List<AckName>();

            foreach (var n1 in n1Loop)
            {
                var n2 = n1.Segments.FirstOrDefault(x => x.SegmentId == "N2");
                var n3 = n1.Segments.FirstOrDefault(x => x.SegmentId == "N3");
                var n4 = n1.Segments.FirstOrDefault(x => x.SegmentId == "N4");

                var name = new AckName();

                name.N101_EntityIdentifierCode = n1.GetElement(1);
                name.N102_Name = n1.GetElement(2);
                name.N103_IdentificationCodeQualifier = n1.GetElement(3);
                name.N104_IdentificationCode = n1.GetElement(4);

                if (n2 != null)
                {
                    name.N201_Name = n2.GetElement(1);
                    name.N202_Name = n2.GetElement(2);
                }

                if (n3 != null)
                {
                    name.N301_Address = n3.GetElement(1);
                    name.N302_Address = n3.GetElement(2);
                }

                if (n4 != null)
                {
                    name.N401_City = n4.GetElement(1);
                    name.N402_State = n4.GetElement(2);
                    name.N403_PostalCode = n4.GetElement(3);
                    name.N404_Country = n4.GetElement(4);
                }

                names.Add(name);
            }
            return names;
        }

        private string GetCustomerId(List<AckName> names)
        {
            var name = names.FirstOrDefault(x => x.N101_EntityIdentifierCode == "BY");
            if (name != null)
            {
                return name.N104_IdentificationCode;
            }

            name = names.FirstOrDefault(x => x.N101_EntityIdentifierCode == "BT");
            if (name != null)
            {
                return name.N104_IdentificationCode;
            }

            return "";
        }
    }
}

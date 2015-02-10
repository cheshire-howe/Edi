using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Edi.Dal.Interfaces;
using Edi.Logic.Interfaces;
using Edi.Models.InvoiceModels;
using EdiTools;
using OopFactory.X12.Parsing;
using OopFactory.X12.Parsing.Model;

namespace Edi.Logic.Concrete
{
    public class InvoiceLogic : IInvoiceLogic
    {
        public void WriteInvoiceEdi(Invoice invoice)
        {
            var ediDocument = new EdiDocument();
            var isa = new EdiSegment("ISA");
            isa[01] = "00";
            isa[02] = "".PadRight(10);
            isa[03] = "00";
            isa[04] = "".PadRight(10);
            isa[05] = "ZZ";
            isa[06] = "SENDER".PadRight(15);
            isa[07] = "ZZ";
            isa[08] = "RECEIVER".PadRight(15);
            isa[09] = EdiValue.Date(6, DateTime.Now);
            isa[10] = EdiValue.Time(4, DateTime.Now);
            isa[11] = "U";
            isa[12] = "00400";
            isa[13] = 1.ToString("d9");
            isa[14] = "0";
            isa[15] = "P";
            isa[16] = ">";
            ediDocument.Segments.Add(isa);

            var gs = new EdiSegment("GS");
            gs[01] = "IN";
            gs[02] = "SENDER";
            gs[03] = "RECEIVER";
            gs[04] = EdiValue.Date(8, DateTime.Now);
            gs[05] = EdiValue.Time(4, DateTime.Now);
            gs[06] = EdiValue.Numeric(0, 1);
            gs[07] = "X";
            gs[08] = "004010";
            ediDocument.Segments.Add(gs);

            var st = new EdiSegment("ST");
            st[01] = "810";
            st[02] = "166061414";
            ediDocument.Segments.Add(st);

            var big = new EdiSegment("BIG");
            big[01] = invoice.BIG01_Date != null
                ? invoice.BIG01_Date.Value.ToString("yyyyMMdd")
                : "";
            big[02] = invoice.BIG02_InvoiceNumber;
            big[03] = invoice.BIG03_Date != null
                ? invoice.BIG01_Date.Value.ToString("yyyyMMdd")
                : "";
            big[04] = invoice.BIG04_PurchaseOrderNumber;
            big[07] = invoice.BIG04_TransactionTypeCode;
            big[08] = invoice.BIG08_TransactionSetPurposeCode;
            ediDocument.Segments.Add(big);

            foreach (var note in invoice.Notes)
            {
                var nte = new EdiSegment("NTE");
                nte[01] = note.NTE01_NoteReferenceCode;
                nte[02] = note.NTE02_NoteDescription;
                ediDocument.Segments.Add(nte);
            }

            if (!(String.IsNullOrEmpty(invoice.CUR01_CurrencyEntityIdentifierCode)
                && String.IsNullOrEmpty(invoice.CUR02_CurrencyCode)
                && String.IsNullOrEmpty(invoice.CUR03_ExchangeRate)))
            {
                var cur = new EdiSegment("CUR");
                cur[01] = invoice.CUR01_CurrencyEntityIdentifierCode;
                cur[02] = invoice.CUR02_CurrencyCode;
                ediDocument.Segments.Add(cur);
            }

            foreach (var invoiceRef in invoice.Refs)
            {
                var refinv = new EdiSegment("REF");
                refinv[01] = invoiceRef.REF01_ReferenceIdentificationQualifier;
                refinv[02] = invoiceRef.REF02_ReferenceIdentification;
                refinv[03] = invoiceRef.REF03_Description;
                ediDocument.Segments.Add(refinv);
            }

            foreach (var name in invoice.Names)
            {
                var n1 = new EdiSegment("N1");
                n1[01] = name.N101_EntityIdentifierCode;
                n1[02] = name.N102_Name;
                n1[03] = name.N103_IdentificationCodeQualifier;
                n1[04] = name.N104_IdentificationCode;
                ediDocument.Segments.Add(n1);

                if (!(String.IsNullOrEmpty(name.N201_Name)
                    && String.IsNullOrEmpty(name.N202_Name)))
                {
                    var n2 = new EdiSegment("N2");
                    n2[01] = name.N201_Name;
                    n2[02] = name.N202_Name;
                    ediDocument.Segments.Add(n2);
                }

                if (!(String.IsNullOrEmpty(name.N301_Address)
                    && String.IsNullOrEmpty(name.N302_Address)))
                {
                    var n3 = new EdiSegment("N3");
                    n3[01] = name.N301_Address;
                    n3[02] = name.N302_Address;
                    ediDocument.Segments.Add(n3);
                }

                if (!(String.IsNullOrEmpty(name.N401_City)
                    && String.IsNullOrEmpty(name.N402_State)
                    && String.IsNullOrEmpty(name.N403_PostalCode)
                    && String.IsNullOrEmpty(name.N404_Country)))
                {
                    var n4 = new EdiSegment("N4");
                    n4[01] = name.N401_City;
                    n4[02] = name.N402_State;
                    n4[03] = name.N403_PostalCode;
                    n4[04] = name.N404_Country;
                    ediDocument.Segments.Add(n4);
                }
            }

            var itd = new EdiSegment("ITD");
            itd[01] = invoice.ITD01_TermsTypeCode;
            itd[02] = invoice.ITD02_TermsBasisDateCode;
            itd[07] = invoice.ITD07_TermsNetDays != null
                ? invoice.ITD07_TermsNetDays.Value.ToString()
                : "";
            itd[12] = invoice.ITD12_Description;
            ediDocument.Segments.Add(itd);

            var dtm = new EdiSegment("DTM");
            dtm[01] = invoice.DTM01_DateTimeQualifier;
            dtm[02] = invoice.DTM02_ShipDate != null
                ? invoice.DTM02_ShipDate.Value.ToString("yyyyMMdd")
                : "";
            ediDocument.Segments.Add(dtm);

            foreach (var item in invoice.Items)
            {
                var it1 = new EdiSegment("IT1");
                it1[01] = item.IT101_AssignedIdentification;
                it1[02] = item.IT102_QuantityInvoiced != null
                    ? item.IT102_QuantityInvoiced.ToString()
                    : "";
                it1[03] = item.IT103_UnitOfMeasurement;
                it1[04] = item.IT104_UnitPrice != null
                    ? item.IT104_UnitPrice.Value.ToString("0.00")
                    : "";
                it1[05] = item.IT105_BasisOfUnitPriceCode;
                it1[06] = item.IT106_ProductIdQualifier;
                it1[07] = item.IT107_ProductID;
                it1[08] = item.IT108_ProductIdQualifier;
                it1[09] = item.IT109_ProductID;
                it1[10] = item.IT110_ProductIdQualifier;
                it1[11] = item.IT111_ProductID;
                ediDocument.Segments.Add(it1);

                var pid = new EdiSegment("PID");
                pid[01] = item.PID01_ItemDescriptionType;
                pid[05] = item.PID05_ItemDescription;
                ediDocument.Segments.Add(pid);
            }

            var tds = new EdiSegment("TDS");
            tds[01] = invoice.TDS01_Amount != null
                ? invoice.TDS01_Amount.Value.ToString("0.00")
                : "";
            ediDocument.Segments.Add(tds);

            var ctt = new EdiSegment("CTT");
            ctt[01] = invoice.CTT01_TransactionTotals != null
                ? invoice.CTT01_TransactionTotals.ToString()
                : "";

            var se = new EdiSegment("SE");
            se[01] = (ediDocument.Segments.Count - 1).ToString();
            se[02] = st[02];
            ediDocument.Segments.Add(se);

            var ge = new EdiSegment("GE");
            ge[01] = EdiValue.Numeric(0, 1);
            ge[02] = gs[06];
            ediDocument.Segments.Add(ge);

            var iea = new EdiSegment("IEA");
            iea[01] = EdiValue.Numeric(0, 1);
            iea[02] = isa[13];
            ediDocument.Segments.Add(iea);

            ediDocument.Options.SegmentTerminator = '~';
            ediDocument.Options.ElementSeparator = '*';
            ediDocument.Save(@"..\..\..\Invoice.txt");
        }

        public Invoice ConvertInvoice(FileStream fs)
        {
            var parser = new X12Parser();
            var interchanges = parser.ParseMultiple(fs);

            // Edi section ISA
            var isa = interchanges[0];
            // Edi section GS
            var gs = isa.FunctionGroups.ToList()[0];
            // Edi section ST
            var st = gs.Transactions[0];
            // Edi section BIG
            var big = st.Segments.FirstOrDefault(x => x.SegmentId == "BIG");
            // Edi section CUR
            var cur = st.Segments.FirstOrDefault(x => x.SegmentId == "CUR");
            // Edi section ITD
            var itd = st.Segments.FirstOrDefault(x => x.SegmentId == "ITD");
            // Edi section DTM
            var dtm = st.Segments.FirstOrDefault(x => x.SegmentId == "DTM");
            // Edi section TDS
            var tds = st.Segments.FirstOrDefault(x => x.SegmentId == "TDS");
            // Edi section CTT
            var ctt = st.Segments.FirstOrDefault(x => x.SegmentId == "CTT");
            // Edi section N1 - loop
            var n1 = st.Loops.Where(x => x.SegmentId == "N1").ToList();
            // Edi section NTE
            var nte = st.Segments.Where(x => x.SegmentId == "NTE").ToList();
            // Edi section IT1 - loop
            var it1 = st.Loops.Where(x => x.SegmentId == "IT1").ToList();
            // Edi section REF - (ref keyword is taken so variable name can't follow convention)
            var refinv = st.Segments.Where(x => x.SegmentId == "REF").ToList();

            var names = ExtractNames(n1);
            var notes = ExtractNotes(nte);
            var items = ExtractItems(it1);
            var refs = ExtractRefs(refinv);

            var invoice = new Invoice()
            {
                CustomerID = GetCustomerId(names),
                BIG01_Date = big != null ? big.GetDate8Element(1) : null,
                BIG02_InvoiceNumber = big != null ? big.GetElement(2) : null,
                BIG03_Date = big != null ? big.GetDate8Element(1) : null,
                BIG04_PurchaseOrderNumber = big != null ? big.GetElement(4) : null,
                BIG04_TransactionTypeCode = big != null ? big.GetElement(7) : null,
                BIG08_TransactionSetPurposeCode = big != null ? big.GetElement(8) : null,
                CUR01_CurrencyEntityIdentifierCode = cur != null ? cur.GetElement(1) : null,
                CUR02_CurrencyCode = cur != null ? cur.GetElement(2) : null,
                CUR03_ExchangeRate = cur != null ? cur.GetElement(3) : null,
                ITD01_TermsTypeCode = itd != null ? itd.GetElement(1) : null,
                ITD02_TermsBasisDateCode = itd != null ? itd.GetElement(2) : null,
                ITD07_TermsNetDays = itd != null ? itd.GetIntElement(7) : null,
                ITD12_Description = itd != null ? itd.GetElement(12) : null,
                DTM01_DateTimeQualifier = dtm != null ? dtm.GetElement(1) : null,
                DTM02_ShipDate = dtm != null ? dtm.GetDate8Element(2) : null,
                TDS01_Amount = tds != null ? tds.GetDecimalElement(1) : null,
                CTT01_TransactionTotals = ctt != null ? ctt.GetIntElement(1) : null,
                Names = names,
                Notes = notes,
                Items = items,
                Refs = refs,
                InvoiceEnvelope = new InvoiceEnvelope()
                {
                    ISA01_AuthInfoQualifier = "O",
                    ISA02_AuthInfo = "k"
                }
            };

            Console.WriteLine(isa.SerializeToX12(true));

            return invoice;
        }

        private List<InvoiceRef> ExtractRefs(List<Segment> invref)
        {
            var refs = new List<InvoiceRef>();

            foreach (var seg in invref)
            {
                var reference = new InvoiceRef()
                {
                    REF01_ReferenceIdentificationQualifier = seg.GetElement(1),
                    REF02_ReferenceIdentification = seg.GetElement(2),
                    REF03_Description = seg.GetElement(3)
                };

                refs.Add(reference);
            }

            return refs;
        }

        private List<InvoiceNote> ExtractNotes(List<Segment> nte)
        {
            var notes = new List<InvoiceNote>();

            foreach (var seg in nte)
            {
                var note = new InvoiceNote()
                {
                    NTE01_NoteReferenceCode = seg.GetElement(1),
                    NTE02_NoteDescription = seg.GetElement(2)
                };

                notes.Add(note);
            }

            return notes;
        }

        private List<InvoiceItem> ExtractItems(List<Loop> it1Loop)
        {
            var items = new List<InvoiceItem>();

            foreach (var it1 in it1Loop)
            {
                var pid = it1.Segments.FirstOrDefault(x => x.SegmentId == "PID");

                var item = new InvoiceItem();

                item.IT101_AssignedIdentification = it1.GetElement(1);
                item.IT102_QuantityInvoiced = it1.GetIntElement(2);
                item.IT103_UnitOfMeasurement = it1.GetElement(3);
                item.IT104_UnitPrice = it1.GetDecimalElement(4);
                item.IT105_BasisOfUnitPriceCode = it1.GetElement(5);
                item.IT106_ProductIdQualifier = it1.GetElement(6);
                item.IT107_ProductID = it1.GetElement(7);
                item.IT108_ProductIdQualifier = it1.GetElement(8);
                item.IT109_ProductID = it1.GetElement(9);
                item.IT110_ProductIdQualifier = it1.GetElement(10);
                item.IT111_ProductID = it1.GetElement(11);

                if (pid != null)
                {
                    item.PID01_ItemDescriptionType = pid.GetElement(1);
                    item.PID05_ItemDescription = pid.GetElement(5);
                }

                items.Add(item);
            }

            return items;
        }

        private List<InvoiceName> ExtractNames(List<Loop> n1Loop)
        {
            var names = new List<InvoiceName>();

            foreach (var n1 in n1Loop)
            {
                var n2 = n1.Segments.FirstOrDefault(x => x.SegmentId == "N2");
                var n3 = n1.Segments.FirstOrDefault(x => x.SegmentId == "N3");
                var n4 = n1.Segments.FirstOrDefault(x => x.SegmentId == "N4");

                var name = new InvoiceName();

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

        private string GetCustomerId(List<InvoiceName> names)
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

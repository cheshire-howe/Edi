using System.Net;
using Edi.Core;
using Edi.Dal.Interfaces;
using Edi.Logic.Interfaces;
using Edi.Models.PurchaseOrderModels;
using EdiTools;
using OopFactory.X12.Parsing;
using OopFactory.X12.Parsing.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Edi.Logic.Concrete
{
    public class PurchaseOrderLogic : IPurchaseOrderLogic
    {
        // Dependency injection with multiple dbContexts
        // This cannot use the InvoiceRepository
        public PurchaseOrderLogic()
        {
        }

        public string WritePurchaseOrderEdi(PurchaseOrder purchaseOrder)
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
            isa[12] = "00401";
            isa[13] = 1.ToString("d9");
            isa[14] = "0";
            isa[15] = "P";
            isa[16] = ">";
            ediDocument.Segments.Add(isa);

            var gs = new EdiSegment("GS");
            gs[01] = "PO";
            gs[02] = "SENDER".PadRight(15);
            gs[03] = "RECEIVER".PadRight(15);
            gs[04] = EdiValue.Date(8, DateTime.Now);
            gs[05] = EdiValue.Time(4, DateTime.Now);
            gs[06] = EdiValue.Numeric(0, 1);
            gs[07] = "X";
            gs[08] = "004010";
            ediDocument.Segments.Add(gs);

            var st = new EdiSegment("ST");
            st[01] = "850";
            st[02] = "166061414";
            ediDocument.Segments.Add(st);

            var beg = new EdiSegment("BEG");
            beg[01] = purchaseOrder.BEG01_TransactionSetPurposeCode;
            beg[02] = purchaseOrder.BEG02_PurchaseOrderTypeCode;
            beg[03] = purchaseOrder.BEG03_PurchaseOrderNumber;
            beg[04] = "";
            beg[05] = purchaseOrder.BEG05_Date != null
                ? purchaseOrder.BEG05_Date.Value.ToString("yyyyMMdd")
                : "";
            ediDocument.Segments.Add(beg);

            if (!(String.IsNullOrEmpty(purchaseOrder.CUR01_CurrencyEntityIdentifierCode)
                && String.IsNullOrEmpty(purchaseOrder.CUR02_CurrencyCode)))
            {
                var cur = new EdiSegment("CUR");
                cur[01] = purchaseOrder.CUR01_CurrencyEntityIdentifierCode;
                cur[02] = purchaseOrder.CUR02_CurrencyCode;
                ediDocument.Segments.Add(cur);
            }

            foreach (var poRef in purchaseOrder.Refs)
            {
                var refpo = new EdiSegment("REF");
                refpo[01] = poRef.REF01_ReferenceIdentificationQualifier;
                refpo[02] = poRef.REF02_ReferenceIdentification;
                refpo[03] = poRef.REF03_Description;
                ediDocument.Segments.Add(refpo);
            }
            foreach (var dtm in purchaseOrder.Dtms)
            {
                var dtmpo = new EdiSegment("DTM");
                dtmpo[01] = dtm.DTM01_DateTimeQualifier;
                dtmpo[02] = dtm.DTM02_PurchaseOrderDate != null
                ? dtm.DTM02_PurchaseOrderDate.Value.ToString("yyyyMMdd")
                : "";
                ediDocument.Segments.Add(dtmpo);
            }

            foreach (var name in purchaseOrder.Names)
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
                foreach (var nameRef in name.Refs)
                {
                    var refname = new EdiSegment("REF");
                    refname[01] = nameRef.REF01_ReferenceIdentificationQualifier;
                    refname[02] = nameRef.REF02_ReferenceIdentification;
                    refname[03] = nameRef.REF03_Description;
                    ediDocument.Segments.Add(refname);
                }
            }


            foreach (var item in purchaseOrder.Items)
            {
                var po1 = new EdiSegment("PO1");
                po1[01] = item.PO101_AssignedIdentification;
                po1[02] = item.PO102_QuantityOrdered != null
                    ? item.PO102_QuantityOrdered.ToString()
                    : "";
                po1[03] = item.PO103_UnitOfMeasurement;
                po1[04] = item.PO104_UnitPrice != null
                    ? item.PO104_UnitPrice.Value.ToString("0.00")
                    : "";
                po1[05] = item.PO105_BasisOfUnitPriceCode;
                po1[06] = item.PO106_ProductIdQualifier;
                po1[07] = item.PO107_ProductID;
                po1[08] = item.PO108_ProductIdQualifier;
                po1[09] = item.PO109_ProductID;
                ediDocument.Segments.Add(po1);

                var cur = new EdiSegment("CUR");
                cur[01] = item.CUR01_CurrencyEntityIdentifierCode;
                cur[02] = item.CUR02_CurrencyCode;
                ediDocument.Segments.Add(cur);

                var itref = new EdiSegment("REF");
                itref[01] = item.REF01_ReferenceIdentificationQualifier;
                itref[02] = item.REF02_ReferenceIdentification;
                ediDocument.Segments.Add(itref);

                foreach (var dtm in item.Dtms)
                {
                    var dtmit = new EdiSegment("DTM");
                    dtmit[01] = dtm.DTM01_DateTimeQualifier;
                    dtmit[02] = dtm.DTM02_PurchaseOrderDate != null
                    ? dtm.DTM02_PurchaseOrderDate.Value.ToString("yyyyMMdd")
                    : "";
                    ediDocument.Segments.Add(dtmit);
                }
                foreach (var name in purchaseOrder.Names)
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
                    foreach (var nameRef in name.Refs)
                    {
                        var refname = new EdiSegment("REF");
                        refname[01] = nameRef.REF01_ReferenceIdentificationQualifier;
                        refname[02] = nameRef.REF02_ReferenceIdentification;
                        refname[03] = nameRef.REF03_Description;
                        ediDocument.Segments.Add(refname);
                    }
                }
            }



            var ctt = new EdiSegment("CTT");
            ctt[01] = purchaseOrder.CTT01_NumberofLineItems != null
                ? purchaseOrder.CTT01_NumberofLineItems.ToString()
                : "";
            ediDocument.Segments.Add(ctt);
            var amt = new EdiSegment("AMT");
            amt[01] = purchaseOrder.AMT01_AmountQualifierCode;
            amt[02] = purchaseOrder.AMT02_Amount != null
                ? purchaseOrder.AMT02_Amount.Value.ToString("0.00")
                : "";

            ediDocument.Segments.Add(amt);

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

            var filename = Path.Combine(Settings.EdiTextFileDirectory, "SENDER_" + DateTime.Now.ToString("yyyy-mm-dd_hh.mm.ss") + ".txt");

            ediDocument.Save(filename);

            return filename;
        }

        public void SendPurchaseOrder(string filename)
        {
            try
            {
                var file = new FileInfo(filename);

                using (var client = new WebClient())
                {
                    string ftpUsername = "", ftpPassword = "";
                    var localFilePath = file.FullName;
                    client.Credentials = new NetworkCredential(ftpUsername, ftpPassword);
                    byte[] responseArray = client.UploadFile("ftp://ftpserver.mine.bz/" + file.Name, "STOR", localFilePath);

                    // Decode and display the response.
                    Console.WriteLine("\nResponse Received.The contents of the file uploaded are:\n{0}",
                        System.Text.Encoding.ASCII.GetString(responseArray));
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine("Failed to upload " + ex.ToString());
            }
            catch (Exception)
            {
                Console.WriteLine("An error has occured");
            }

        }

        public PurchaseOrder ConvertPurchaseOrder(List<Interchange> interchanges)
        {
            /*var parser = new X12Parser();
            var interchanges = parser.ParseMultiple(fs);*/

            // Edi section ISA
            var isa = interchanges[0];
            // Edi section GS
            var gs = isa.FunctionGroups.ToList()[0];
            // Edi section ST
            var st = gs.Transactions[0];
            // Edi section BIG
            var beg = st.Segments.FirstOrDefault(x => x.SegmentId == "BEG");
            // Edi section CUR
            var cur = st.Segments.FirstOrDefault(x => x.SegmentId == "CUR");

            // Edi section DTM
            var dtm = st.Segments.Where(x => x.SegmentId == "DTM").ToList();
            // Edi section CTT
            //var ctt = st.Loops.Where(x => x.SegmentId == "CTT").ToList();
            var ctt = st.Loops.Where(x => x.SegmentId == "CTT").ToList()[0];
            var amt = ctt.Segments.ToList()[0];
            // Edi section N1 - loop
            var n1 = st.Loops.Where(x => x.SegmentId == "N1").ToList();
            // Edi section IT1 - loop
            var po1 = st.Loops.Where(x => x.SegmentId == "PO1").ToList();
            // Edi section REF - (ref keyword is taken so variable name can't follow convention)
            var refpo = st.Segments.Where(x => x.SegmentId == "REF").ToList();

            var env = ExtractEnv(isa, gs, st);
            var names = ExtractNames(n1);
            //var ctts = ExtractCtts(ctt);
            var items = ExtractItems(po1);
            var refs = ExtractRefs(refpo);
            var dtms = ExtractPoIDtms(dtm);

            var purchaseOrder = new PurchaseOrder()
            {
                PoEnvelope = env,
                CustomerID = GetCustomerId(names),
                BEG01_TransactionSetPurposeCode = beg != null ? beg.GetElement(1) : null,
                BEG02_PurchaseOrderTypeCode = beg != null ? beg.GetElement(2) : null,
                BEG03_PurchaseOrderNumber = beg != null ? beg.GetElement(3) : null,
                BEG05_Date = beg != null ? beg.GetDate8Element(5) : null,
                CUR01_CurrencyEntityIdentifierCode = cur != null ? cur.GetElement(1) : null,
                CUR02_CurrencyCode = cur != null ? cur.GetElement(2) : null,
                CTT01_NumberofLineItems = ctt.GetIntElement(1),
                AMT01_AmountQualifierCode = amt.GetElement(1),
                AMT02_Amount = amt.GetDecimalElement(2),
                Names = names,
                Dtms = dtms,
                Items = items,
                Refs = refs
            };

            Console.WriteLine(isa.SerializeToX12(true));
            return purchaseOrder;

        }

        private PoEnvelope ExtractEnv(Interchange isa, FunctionGroup gs, Transaction st)
        {
            return new PoEnvelope()
            {
                ISA01_AuthInfoQualifier = isa.AuthorInfoQualifier,
                ISA02_AuthInfo = isa.AuthorInfo,
                ISA03_SecurityInfoQualifier = isa.SecurityInfoQualifier,
                ISA04_SecurityInfo = isa.SecurityInfo,
                ISA05_InterchangeSenderIdQualifier = isa.InterchangeSenderIdQualifier,
                ISA06_InterchangeSenderId = isa.InterchangeSenderId,
                ISA07_InterchangeReceiverIdQualifier = isa.InterchangeReceiverIdQualifier,
                ISA08_InterchangeReceiverId = isa.InterchangeReceiverId,
                ISA09_Date = isa.InterchangeDate,

                ISA10_Time = isa.GetElement(10),
                ISA11_InterchangeControlStandardsIdentifier = isa.GetElement(11),
                ISA12_InterchangeControlVersionNumber = isa.GetElement(12),
                ISA13_InterchangeControlNumber = isa.InterchangeControlNumber,
                ISA14_AcknowledgmentRequested = isa.GetElement(14),
                ISA15_UsageIndicator = isa.GetElement(15),
                ISA16_ComponentElementSeparator = isa.GetElement(16),
                IEA01_NumberOfIncludedFunctionalGroups = (isa.TrailerSegments.ToList()[0]).GetElement(1),
                IEA02_InterchangeControlNumber = (isa.TrailerSegments.ToList()[0]).GetElement(2),

                GS01_FunctionalIdentifierCode = gs.FunctionalIdentifierCode,
                GS02_ApplicationSenderCode = gs.ApplicationSendersCode,
                GS03_ApplicationReceiverCode = gs.ApplicationReceiversCode,
                GS04_Date = gs.Date,
                GS06_GroupControlNumber = gs.ControlNumber.ToString(),
                GS07_ResponsibleAgencyCode = gs.ResponsibleAgencyCode,
                GS08_Version = gs.VersionIdentifierCode,
                GS05_Time = gs.GetElement(5),
                GE01_NumberOfTransactionSetsIncluded = (gs.TrailerSegments.ToList()[0]).GetElement(1),
                GE02_GroupControlNumber = (gs.TrailerSegments.ToList()[0]).GetElement(2),

                ST01_TransactionSetIdentifierCode = st.IdentifierCode,
                ST02_TransactionSetControlNumber = st.ControlNumber,
                SE01_NumberOfIncludedSegments = (st.TrailerSegments.ToList()[0]).GetElement(1),
                SE02_TransactionSetControlNumber = (st.TrailerSegments.ToList()[0]).GetElement(2)
            };
        }

        private List<PoRef> ExtractRefs(List<Segment> poref)
        {
            var refs = new List<PoRef>();

            foreach (var seg in poref)
            {
                var reference = new PoRef()
                {
                    REF01_ReferenceIdentificationQualifier = seg.GetElement(1),
                    REF02_ReferenceIdentification = seg.GetElement(2),
                    REF03_Description = seg.GetElement(3)
                };

                refs.Add(reference);
            }

            return refs;
        }

        private List<PoDtm> ExtractPoIDtms(List<Segment> poDtm)
        {
            var poIDtms = new List<PoDtm>();

            foreach (var seg in poDtm)
            {
                var dtm = new PoDtm()
                {
                    DTM01_DateTimeQualifier = seg.GetElement(1),
                    DTM02_PurchaseOrderDate = seg.GetDate8Element(2),
                };

                poIDtms.Add(dtm);
            }

            return poIDtms;
        }

        private List<PoItem> ExtractItems(List<Loop> po1Loop)
        {
            var items = new List<PoItem>();

            foreach (var po1 in po1Loop)
            {
                var cur = po1.Segments.FirstOrDefault(x => x.SegmentId == "CUR");
                var refpo = po1.Segments.FirstOrDefault(x => x.SegmentId == "REF");
                var poItemDtm = po1.Segments.Where(x => x.SegmentId == "DTM").ToList();
                // Edi section N1 - loop
                var poItemName = po1.Loops.Where(x => x.SegmentId == "N1").ToList();

                var item = new PoItem();

                item.PO101_AssignedIdentification = po1.GetElement(1);
                item.PO102_QuantityOrdered = po1.GetIntElement(2);
                item.PO103_UnitOfMeasurement = po1.GetElement(3);
                item.PO104_UnitPrice = po1.GetDecimalElement(4);
                item.PO105_BasisOfUnitPriceCode = po1.GetElement(5);
                item.PO106_ProductIdQualifier = po1.GetElement(6);
                item.PO107_ProductID = po1.GetElement(7);
                item.PO108_ProductIdQualifier = po1.GetElement(8);
                item.PO109_ProductID = po1.GetElement(9);

                if (cur != null)
                {
                    item.CUR01_CurrencyEntityIdentifierCode = cur.GetElement(1);
                    item.CUR02_CurrencyCode = cur.GetElement(2);
                }

                if (refpo != null)
                {
                    item.REF01_ReferenceIdentificationQualifier = refpo.GetElement(1);
                    item.REF02_ReferenceIdentification = refpo.GetElement(2);
                }
                item.Dtms = ExtractPoItemDtms(poItemDtm);
                item.Names = ExtractPoItemNames(poItemName);
                items.Add(item);
            }

            return items;
        }
        private List<PoItemDtm> ExtractPoItemDtms(List<Segment> poItemDtm)
        {
            var poItemDtms = new List<PoItemDtm>();

            foreach (var seg in poItemDtm)
            {
                var dtm = new PoItemDtm()
                {
                    DTM01_DateTimeQualifier = seg.GetElement(1),
                    DTM02_PurchaseOrderDate = seg.GetDate8Element(2),
                };

                poItemDtms.Add(dtm);
            }

            return poItemDtms;
        }
        private List<PoItemName> ExtractPoItemNames(List<Loop> n1Loop)
        {
            var names = new List<PoItemName>();

            foreach (var n1 in n1Loop)
            {
                var n2 = n1.Segments.FirstOrDefault(x => x.SegmentId == "N2");
                var n3 = n1.Segments.FirstOrDefault(x => x.SegmentId == "N3");
                var n4 = n1.Segments.FirstOrDefault(x => x.SegmentId == "N4");
                var poItemNameRef = n1.Segments.Where(x => x.SegmentId == "REF").ToList();

                var name = new PoItemName();

                name.N101_EntityIdentifierCode = n1.GetElement(1);
                name.N102_Name = n1.GetElement(2);
                name.N103_IdentificationCodeQualifier = n1.GetElement(3);
                name.N104_IdentificationCode = n1.GetElement(4);

                if (n2 != null)
                {
                    name.N201_Name = n2.GetElement(1);
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
                name.Refs = ExtractPoItemNameRefs(poItemNameRef);
                names.Add(name);
            }
            return names;
        }

        private List<PoItemNameRef> ExtractPoItemNameRefs(List<Segment> poItemNameref)
        {
            var refs = new List<PoItemNameRef>();

            foreach (var seg in poItemNameref)
            {
                var reference = new PoItemNameRef()
                {
                    REF01_ReferenceIdentificationQualifier = seg.GetElement(1),
                    REF02_ReferenceIdentification = seg.GetElement(2),
                    REF03_Description = seg.GetElement(3)
                };

                refs.Add(reference);
            }

            return refs;
        }

        private List<PoName> ExtractNames(List<Loop> n1Loop)
        {
            var names = new List<PoName>();

            foreach (var n1 in n1Loop)
            {
                var n2 = n1.Segments.FirstOrDefault(x => x.SegmentId == "N2");
                var n3 = n1.Segments.FirstOrDefault(x => x.SegmentId == "N3");
                var n4 = n1.Segments.FirstOrDefault(x => x.SegmentId == "N4");
                var poNameRef = n1.Segments.Where(x => x.SegmentId == "REF").ToList();

                var name = new PoName();

                name.N101_EntityIdentifierCode = n1.GetElement(1);
                name.N102_Name = n1.GetElement(2);
                name.N103_IdentificationCodeQualifier = n1.GetElement(3);
                name.N104_IdentificationCode = n1.GetElement(4);

                if (n2 != null)
                {
                    name.N201_Name = n2.GetElement(1);
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
                name.Refs = ExtractPoNameRefs(poNameRef);
                names.Add(name);
            }
            return names;
        }

        private List<PoNameRef> ExtractPoNameRefs(List<Segment> poNameref)
        {
            var refs = new List<PoNameRef>();

            foreach (var seg in poNameref)
            {
                var reference = new PoNameRef()
                {
                    REF01_ReferenceIdentificationQualifier = seg.GetElement(1),
                    REF02_ReferenceIdentification = seg.GetElement(2),
                    REF03_Description = seg.GetElement(3)
                };

                refs.Add(reference);
            }

            return refs;
        }
        private string GetCustomerId(List<PoName> names)
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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edi.Logic.Interfaces;
using Edi.Models.AsnModels;
using OopFactory.X12.Parsing;
using OopFactory.X12.Parsing.Model;

namespace Edi.Logic.Concrete
{
    public class AsnLogic : IAsnLogic
    {
        public Asn ConvertAsn(List<Interchange> interchanges)
        {
            // var parser = new X12Parser();
            // var interchanges = parser.ParseMultiple(fs);

            var isa = interchanges[0];
            var gs = isa.FunctionGroups.ToList()[0];
            var st = gs.Transactions[0];
            var bsn = st.Segments.FirstOrDefault(x => x.SegmentId == "BSN");
            var dtm = st.Segments.Where(x => x.SegmentId == "DTM").ToList();
            var ctt = st.Segments.FirstOrDefault(x => x.SegmentId == "CTT");
            var hlOne = st.HLoops.ToList()[0];

            var env = ExtractEnv(isa, gs, st);
            var dtms = ExtractDtms(dtm);
            var hLoopOne = ExtractHlOne(hlOne);

            Console.WriteLine(dtms);
            dtms.ForEach(Console.WriteLine);

            var asn = new Asn()
            {
                AsnEnvelope = env,
                BSN01_TransactionSetPurposeCode = bsn != null ? bsn.GetElement(1) : null,
                BSN02_ShipmentIdentifier = bsn != null ? bsn.GetElement(2) : null,
                BSN03_Date = bsn != null ? bsn.GetDate8Element(3) : null,
                BSN04_Time = bsn != null ? bsn.GetElement(4) : null,
                CTT01_NumberOfLineItems = ctt != null ? ctt.GetElement(1) : null,
                CTT02_HashTotal = ctt != null ? ctt.GetIntElement(2) : null,
                CTT03_Weight = ctt != null ? ctt.GetIntElement(3) : null,
                CTT04_UnitOfMeasurement = ctt != null ? ctt.GetElement(4) : null,
                CTT05_Volume = ctt != null ? ctt.GetIntElement(5) : null,
                CTT06_UnitOfMeasurement = ctt != null ? ctt.GetElement(6) : null,
                CTT07_Description = ctt != null ? ctt.GetElement(7) : null,
                AsnDtms = dtms,
                Shipment = hLoopOne,
            };

            return asn;
        }

        private AsnEnvelope ExtractEnv(Interchange isa, FunctionGroup gs, Transaction st)
        {
            return new AsnEnvelope()
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
                GS01_FunctionalIdentifierCode = gs.FunctionalIdentifierCode,
                GS02_ApplicationSenderCode = gs.ApplicationSendersCode,
                GS03_ApplicationReceiverCode = gs.ApplicationReceiversCode,
                GS04_Date = gs.Date,
                GS06_GroupControlNumber = gs.ControlNumber.ToString(),
                GS07_ResponsibleAgencyCode = gs.ResponsibleAgencyCode,
                GS08_Version = gs.VersionIdentifierCode,
                ST01_TransactionSetIdentifierCode = st.IdentifierCode,
                ST02_TransactionSetControlNumber = st.ControlNumber
            };
        }

        private List<AsnHlOneShipment> ExtractHlOne(HierarchicalLoop hlOne)
        {
            ///////////////////////////////////////////////////////////////
            // Root of Hierarchical Loop
            ///////////////////////////////////////////////////////////////
            var hl = new AsnHlOneShipment()
            {
                HL01_IdNumber = hlOne.Id,
                HL02_ParentId = hlOne.ParentId,
                HL03_LevelCode = hlOne.LevelCode,
                HL04_ChildCode = hlOne.HierarchicalChildCode,
            };

            // TD1 segment
            if (hlOne.Segments.ToList().FirstOrDefault(x => x.SegmentId == "TD1") != null)
            {
                var td = hlOne.Segments.ToList().FirstOrDefault(x => x.SegmentId == "TD1");
                hl.TD101_PackagingCode = td.GetElement(1);
                hl.TD102_LadingQuantity = td.GetElement(2);
            }

            // TD5 segment
            if (hlOne.Segments.ToList().FirstOrDefault(x => x.SegmentId == "TD5") != null)
            {
                var td = hlOne.Segments.ToList().FirstOrDefault(x => x.SegmentId == "TD5");
                hl.TD501_RoutingSequenceCode = td.GetElement(1);
                hl.TD502_IdCodeQualifier = td.GetElement(2);
                hl.TD503_IdCode = td.GetElement(3);
                hl.TD504_TransportationMethodMode = td.GetElement(4);
            }

            // TD3 segment
            if (hlOne.Segments.ToList().FirstOrDefault(x => x.SegmentId == "TD3") != null)
            {
                var td = hlOne.Segments.ToList().FirstOrDefault(x => x.SegmentId == "TD3");
                hl.TD301_EquipmentDescriptionCode = td.GetElement(1);
                hl.TD302_EquipmentInitial = td.GetElement(2);
                hl.TD303_EquipmentNumber = td.GetElement(3);
            }

            // Possible multiple REF segments
            if (hlOne.Segments.ToList().FirstOrDefault(x => x.SegmentId == "REF") != null)
            {
                var asnref = hlOne.Segments.Where(x => x.SegmentId == "REF").ToList();

                var asnrefs = new List<AsnHlOneShipmentRef>();

                foreach (var seg in asnref)
                {
                    var r = new AsnHlOneShipmentRef()
                    {
                        REF01_ReferenceIdentificationQualifier = seg.GetElement(1),
                        REF02_ReferenceIdentification = seg.GetElement(2),
                        REF03_Description = seg.GetElement(3)
                    };

                    asnrefs.Add(r);
                }

                hl.Refs = asnrefs;
            }

            // Possible multiple N1 loops
            if (hlOne.Segments.ToList().FirstOrDefault(x => x.SegmentId == "N1") != null)
            {
                var n1 = hlOne.Loops.Where(x => x.SegmentId == "N1").ToList();
                hl.Names = ExtractNames(n1);
            }

            // Start of chaining to fill out the guts of the hierarchy
            var hlTwos = ExtractHlTwo(hlOne.HLoops.ToList());

            hl.Orders = hlTwos;

            var hLoop = new List<AsnHlOneShipment>();
            hLoop.Add(hl);

            return hLoop;
        }

        private List<AsnHlTwoOrder> ExtractHlTwo(List<HierarchicalLoop> hl2)
        {
            var hlTwos = new List<AsnHlTwoOrder>();
            foreach (var loop in hl2)
            {
                var o = new AsnHlTwoOrder()
                {
                    HL01_IdNumber = loop.Id,
                    HL02_ParentId = loop.ParentId,
                    HL03_LevelCode = loop.LevelCode,
                    HL04_ChildCode = loop.HierarchicalChildCode
                };

                var prf = loop.Segments.FirstOrDefault(x => x.SegmentId == "PRF");
                if (prf != null)
                {
                    o.PRF01_PurchaseOrderNumber = prf.GetElement(1);
                    o.PRF02_Date = prf.GetDate8Element(2);
                }

                var hlThrees = ExtractHlThree(loop.HLoops.ToList());

                o.Items = hlThrees;

                hlTwos.Add(o);
            }

            return hlTwos;
        }

        private List<AsnHlThreeItem> ExtractHlThree(List<HierarchicalLoop> hl3)
        {
            var hlThrees = new List<AsnHlThreeItem>();
            foreach (var loop in hl3)
            {
                var i = new AsnHlThreeItem()
                {
                    HL01_IdNumber = loop.Id,
                    HL02_ParentId = loop.ParentId,
                    HL03_LevelCode = loop.LevelCode,
                    HL04_ChildCode = loop.HierarchicalChildCode
                };

                var lin = loop.Segments.FirstOrDefault(x => x.SegmentId == "LIN");
                if (lin != null)
                {
                    i.LIN01_AssignedIdentification = lin.GetElement(1);
                    i.LIN02_IdQualifier = lin.GetElement(2);
                    i.LIN03_Id = lin.GetElement(3);
                    i.LIN04_IdQualifier = lin.GetElement(4);
                    i.LIN05_Id = lin.GetElement(5);
                    i.LIN06_IdQualifier = lin.GetElement(6);
                    i.LIN07_Id = lin.GetElement(7);
                }

                var sn1 = loop.Segments.FirstOrDefault(x => x.SegmentId == "SN1");
                if (sn1 != null)
                {
                    i.SN101_AssignedIdentification = sn1.GetElement(1);
                    i.SN102_NumberOfUnitsShipped = sn1.GetIntElement(2);
                    i.SN103_UnitOfMeasurementCode = sn1.GetElement(3);
                    i.SN105_QuantityOrdered = sn1.GetIntElement(5);
                    i.SN106_UnitOfMeasurementCode = sn1.GetElement(6);
                }

                var pid = loop.Segments.FirstOrDefault(x => x.SegmentId == "PID");
                if (pid != null)
                {
                    i.PID01_ItemDescriptionType = pid.GetElement(1);
                    i.PID05_Description = pid.GetElement(5);
                }

                var mea = loop.Segments.FirstOrDefault(x => x.SegmentId == "MEA");
                if (mea != null)
                {
                    i.MEA01_MeasurementReferenceCode = mea.GetElement(1);
                    i.MEA02_MeasurementQualifier = mea.GetElement(2);
                    i.MEA03_MeasurementValue = mea.GetIntElement(3);
                    i.MEA04_MeasurementReferenceCode = mea.GetElement(4);
                }

                hlThrees.Add(i);
            }

            return hlThrees;
        }

        private List<AsnHlOneShipmentName> ExtractNames(List<Loop> n1Loop)
        {
            var names = new List<AsnHlOneShipmentName>();

            foreach (var n1 in n1Loop)
            {
                var n2 = n1.Segments.FirstOrDefault(x => x.SegmentId == "N2");
                var n3 = n1.Segments.FirstOrDefault(x => x.SegmentId == "N3");
                var n4 = n1.Segments.FirstOrDefault(x => x.SegmentId == "N4");

                var name = new AsnHlOneShipmentName();

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

        private List<AsnDtm> ExtractDtms(List<Segment> dtm)
        {
            var dtms = new List<AsnDtm>();

            foreach (var seg in dtm)
            {
                var d = new AsnDtm()
                {
                    DTM01_DateTimeQualifier = seg.GetElement(1),
                    DTM02_ShipDate = seg.GetDate8Element(2),
                    DTM03_Time = seg.GetElement(3)
                };

                dtms.Add(d);
            }

            return dtms;
        }
    }
}

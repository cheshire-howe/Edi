﻿using Autofac;
using Edi.Dal.Abstract;
using Edi.Dal.Concrete;
using Edi.Dal.Interfaces;
using Edi.Logic.Concrete;
using Edi.Logic.Interfaces;
using Edi.Service.Concrete;
using Edi.Service.Interfaces;

namespace Edi
{
    public class Get
    {
        protected static IContainer Container { get; private set; }

        public static IInvoiceService InvoiceService
        {
            get { return Container.Resolve<IInvoiceService>(); }
        }

        public static IPurchaseOrderService PurchaseOrderService
        {
            get { return Container.Resolve<IPurchaseOrderService>(); }
        }

        public static IAcknowledgmentService AcknowledgmentService
        {
            get { return Container.Resolve<IAcknowledgmentService>(); }
        }

        public static IAsnService AsnService
        {
            get { return Container.Resolve<IAsnService>(); }
        }

        public static IMediationService MediationService
        {
            get { return Container.Resolve<IMediationService>(); }
        }

        public static void Started()
        {
            var builder = new ContainerBuilder();

            builder.RegisterGeneric(typeof(UnitOfWork<>)).As(typeof(IUnitOfWork<>));
            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>));
            builder.RegisterType<InvoiceService>().As<IInvoiceService>();
            builder.RegisterType<InvoiceLogic>().As<IInvoiceLogic>();
            builder.RegisterType<PurchaseOrderService>().As<IPurchaseOrderService>();
            builder.RegisterType<PurchaseOrderLogic>().As<IPurchaseOrderLogic>();
            builder.RegisterType<AcknowledgmentService>().As<IAcknowledgmentService>();
            builder.RegisterType<AcknowledgmentLogic>().As<IAcknowledgmentLogic>();
            builder.RegisterType<AsnService>().As<IAsnService>();
            builder.RegisterType<AsnLogic>().As<IAsnLogic>();
            builder.RegisterType<PartnershipService>().As<IPartnershipService>();
            builder.RegisterType<MediationService>().As<IMediationService>();
            builder.RegisterType<MediationLogic>().As<IMediationLogic>();

            Container = builder.Build();
        }
    }
}

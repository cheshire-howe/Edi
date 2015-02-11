using Autofac;
using Edi.Dal.Abstract;
using Edi.Dal.Concrete;
using Edi.Dal.Interfaces;
using Edi.Logic;
using Edi.Logic.Concrete;
using Edi.Logic.Interfaces;
using Edi.Models.InvoiceModels;
using Edi.Models.PurchaseOrderModels;
using Edi.Service.Concrete;
using Edi.Service.Interfaces;
//using Edi.Service.Concrete;
//using Edi.Service.Interfaces;

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

            Container = builder.Build();
        }
    }
}

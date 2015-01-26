using Autofac;
using Edi.Dal.Abstract;
using Edi.Dal.Concrete;
using Edi.Dal.Interfaces;
using Edi.Logic;
using Edi.Models.InvoiceModels;
using Edi.Models.PurchaseOrderModels;

namespace Edi
{
    public class Get
    {
        protected static IContainer Container { get; private set; }

        public static IInvoiceRepository InvoiceRepository
        {
            get { return Container.Resolve<IUnitOfWork<InvoiceContext>>().InvoiceRepository; }
        }

        public static IPurchaseOrderRepository PurchaseOrderRepository
        {
            get { return Container.Resolve<IUnitOfWork<PurchaseOrderContext>>().PurchaseOrderRepository; }
        }

        public static InvoiceLogic InvoiceLogic
        {
            get { return Container.Resolve<InvoiceLogic>(); }
        }

        public static PurchaseOrderLogic PurchaseOrderLogic
        {
            get { return Container.Resolve<PurchaseOrderLogic>(); }
        }

        public static void Started()
        {
            var builder = new ContainerBuilder();

            builder.RegisterGeneric(typeof(UnitOfWork<>)).As(typeof(IUnitOfWork<>));
            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>));
            builder.RegisterType<InvoiceLogic>();
            builder.RegisterType<PurchaseOrderLogic>();

            Container = builder.Build();
        }
    }
}

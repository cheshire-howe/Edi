using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Edi.Dal.Abstract;
using Edi.Dal.Concrete;
using Edi.Dal.Interfaces;
using Edi.Logic;
using Edi.Logic.Concrete;
using Edi.WebUI.Modules;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Edi.WebUI.Startup))]
namespace Edi.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterGeneric(typeof(UnitOfWork<>)).As(typeof(IUnitOfWork<>));
            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>));
            builder.RegisterModule(new ServiceModule());
            builder.RegisterModule(new LogicModule());
            builder.RegisterType<PurchaseOrderLogic>();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            app.UseAutofacMiddleware(container);
            app.UseAutofacMvc();

            ConfigureAuth(app);
        }
    }
}

using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Edi.Dal.Abstract;
using Edi.Dal.Concrete;
using Edi.Dal.Interfaces;
using Edi.Logic;
using Edi.Logic.Concrete;
using Edi.WebUI.Modules;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartupAttribute(typeof(Edi.WebUI.Startup))]
namespace Edi.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            // Web Api Config
            config.Routes.MapHttpRoute
                (
                    name: "DefaultApi",
                    routeTemplate: "api/{controller}/{id}",
                    defaults: new {id = RouteParameter.Optional}
                );

            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling =
                Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                AuthenticationMode = AuthenticationMode.Active
            });
            
            // SignalR Config
            app.MapSignalR();

            // Autofac Config
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterGeneric(typeof(UnitOfWork<>)).As(typeof(IUnitOfWork<>));
            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>));
            builder.RegisterModule(new ServiceModule());
            builder.RegisterModule(new LogicModule());
            builder.RegisterType<PurchaseOrderLogic>();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            // Register
            app.UseAutofacMiddleware(container);
            app.UseAutofacMvc();

            app.UseWebApi(config);

            ConfigureAuth(app);
        }
    }
}

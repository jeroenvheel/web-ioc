using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.SignalR;
using Autofac.Integration.WebApi;
using Microsoft.AspNet.SignalR;
using Owin;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using web_ioc.Hubs;
using web_ioc.Models;

namespace web_ioc
{
    public static class IocConfig
    {
        internal static IContainer Container { get; set; }

        private static ISessionModel SetupSession(IComponentContext context)
        {
            var httpBase = context.Resolve<HttpContextBase>();

            SessionModel session = null;

            if (httpBase.Session["GV::Session"] == null)
            {
                session = new SessionModel();
                httpBase.Session["GV::Session"] = session;
            }
            else
            {
                session = httpBase.Session["GV::Session"] as SessionModel;
            }

            return session as ISessionModel;
        }

        private static void SetupMvc(ContainerBuilder builder)
        {
            // register all controllers
            builder.RegisterControllers(typeof(IocConfig).Assembly).InstancePerRequest();

            // OPTIONAL: Register model binders that require DI.
            builder.RegisterModelBinders(typeof(IocConfig).Assembly);
            builder.RegisterModelBinderProvider();

            // OPTIONAL: Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();

            // OPTIONAL: Enable property injection in view pages.
            builder.RegisterSource(new ViewRegistrationSource());

            // OPTIONAL: Enable property injection into action filters.
            builder.RegisterFilterProvider();
        }
        private static void SetupWebApi(ContainerBuilder builder)
        {
            // Register all API controller
            builder.RegisterApiControllers(typeof(IocConfig).Assembly).InstancePerRequest();
        }
        private static void SetupHubs(ContainerBuilder builder)
        {
            // Register your SignalR hubs.
            builder.RegisterHubs(typeof(IocConfig).Assembly).ExternallyOwned();
        }
        private static void RegisterTypes(ContainerBuilder builder)
        {
            builder.Register(ctx => SetupSession(ctx)).As<ISessionModel>().InstancePerRequest().ExternallyOwned();
        }

        internal static void SetupContainer(IAppBuilder app)
        {
            var builder = new ContainerBuilder();

            SetupMvc(builder);
            SetupWebApi(builder);
            SetupHubs(builder);
            RegisterTypes(builder);

            Container = builder.Build();

            DependencyResolver.SetResolver(new Autofac.Integration.Mvc.AutofacDependencyResolver(Container));

            var config = GlobalConfiguration.Configuration;
            config.DependencyResolver = new AutofacWebApiDependencyResolver(Container);

            app.UseAutofacMiddleware(Container);

            //var hubconfig = new HubConfiguration
            //{
            //    Resolver = new Autofac.Integration.SignalR.AutofacDependencyResolver(Container)
            //};

            GlobalHost.DependencyResolver = new Autofac.Integration.SignalR.AutofacDependencyResolver(Container);
            //app.MapSignalR("/signalr", hubconfig);

            app.MapSignalR();
            app.UseAutofacMvc();
        }
    }
}
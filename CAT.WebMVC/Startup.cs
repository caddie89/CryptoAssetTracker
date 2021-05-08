using Autofac;
using Autofac.Integration.Mvc;
using CAT.Contracts.Moment_Contract;
using CAT.Services.Moment_Service;
using Microsoft.Owin;
using Owin;
using System.Web.Mvc;

[assembly: OwinStartupAttribute(typeof(CAT.WebMVC.Startup))]
namespace CAT.WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterModule<AutofacWebTypesModule>();
            builder.RegisterType<MomentService>().As<IMomentService>();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            ConfigureAuth(app);
        }
    }
}

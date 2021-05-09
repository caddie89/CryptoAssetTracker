using Autofac;
using Autofac.Integration.Mvc;
using CAT.Contracts.Moment_Contract;
using CAT.Contracts.MomentShowcase_Contract;
using CAT.Contracts.Player_Contract;
using CAT.Contracts.Showcase_Contract;
using CAT.Contracts.SoldMoment_Contract;
using CAT.Data.Entities;
using CAT.Services.Moment_Service;
using CAT.Services.MomentShowcase_Service;
using CAT.Services.Player_Service;
using CAT.Services.Showcase_Service;
using CAT.Services.SoldMoment_Service;
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
            // Create the builder with which components/services are registered.
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            
            builder.RegisterModule<AutofacWebTypesModule>();

            // Register types that expose interfaces...
            builder.RegisterType<PlayerService>().As<IPlayerService>();
            builder.RegisterType<MomentService>().As<IMomentService>();
            builder.RegisterType<ShowcaseService>().As<IShowcaseService>();
            builder.RegisterType<SoldMomentService>().As<ISoldMomentService>();
            builder.RegisterType<MomentShowcaseService>().As<IMomentShowcaseService>();

            // Build the container to finalize registrations
            // and prepare for object resolution.
            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            ConfigureAuth(app);
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CAT.WebMVC.Startup))]
namespace CAT.WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

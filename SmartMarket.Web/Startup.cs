using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SmartMarket.Web.Startup))]
namespace SmartMarket.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

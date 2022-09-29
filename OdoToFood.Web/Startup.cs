using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OdoToFood.Web.Startup))]
namespace OdoToFood.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

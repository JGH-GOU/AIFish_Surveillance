using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OWBS_WebApp.Startup))]
namespace OWBS_WebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

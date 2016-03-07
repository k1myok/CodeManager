using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ShareService.ServiceManager.Startup))]
namespace ShareService.ServiceManager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Ninesky.Startup))]
namespace Ninesky
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OracelEF.Startup))]
namespace OracelEF
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

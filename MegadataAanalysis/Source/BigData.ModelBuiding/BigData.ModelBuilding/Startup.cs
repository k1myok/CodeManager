using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BigData.ModelBuilding.Startup))]
namespace BigData.ModelBuilding
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

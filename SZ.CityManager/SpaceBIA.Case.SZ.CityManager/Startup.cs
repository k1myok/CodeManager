using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute("GuiJianStartup", typeof(SpaceBIA.Case.SZ.CityManager.Startup))]
namespace SpaceBIA.Case.SZ.CityManager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NightRiderMVC.Startup))]
namespace NightRiderMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

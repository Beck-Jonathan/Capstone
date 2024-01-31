using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NightRider.Startup))]
namespace NightRider
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

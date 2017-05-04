using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EasyMarket.Startup))]
namespace EasyMarket
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

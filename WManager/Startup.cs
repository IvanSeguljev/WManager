using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WManager.Startup))]
namespace WManager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

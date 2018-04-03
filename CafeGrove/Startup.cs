using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CafeGrove.Startup))]
namespace CafeGrove
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

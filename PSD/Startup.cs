using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PSD.Startup))]
namespace PSD
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

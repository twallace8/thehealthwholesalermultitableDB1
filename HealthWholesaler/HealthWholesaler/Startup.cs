using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HealthWholesaler.Startup))]
namespace HealthWholesaler
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

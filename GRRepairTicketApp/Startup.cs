using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GRRepairTicketApp.Startup))]
namespace GRRepairTicketApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

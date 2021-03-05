using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BootstrapWebApplication.Startup))]
namespace BootstrapWebApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            // Any connection or hub wire up and configuration should go here
            //System.AppDomain.CurrentDomain.Load(typeof(BootstrapWebApplication.Hubs.MessageHub).Assembly.FullName);
            app.MapSignalR();
        }
    }
}
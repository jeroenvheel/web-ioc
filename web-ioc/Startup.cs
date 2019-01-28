using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(web_ioc.Startup))]
namespace web_ioc
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Any connection or hub wire up and configuration should go here
            IocConfig.SetupContainer(app);
        }
    }
}
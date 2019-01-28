using System.Threading.Tasks;
using Autofac;
using Microsoft.AspNet.SignalR;
using web_ioc.Models;

namespace web_ioc.Hubs
{
    public class TestHub : Hub
    {
        private ILifetimeScope _hubLifetimeScope;

        public TestHub(ILifetimeScope lifetimeScope)
        {
            _hubLifetimeScope = lifetimeScope;
        }

        public void activate()
        {
            Clients.Caller.Activated();
        }

        public override Task OnConnected()
        {
            return base.OnConnected();
        }

    }
}
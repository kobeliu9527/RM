using Dp.Server.Server.SignalR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Dp.Server.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    public class HubController : Controller
    {
        private readonly IHubContext<ChatHub> _hubContext;

        public HubController(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }
        [HttpPost]
        public string SendMsg()
        {
            _hubContext.Clients.All.SendAsync("ReceiveMessage","1",DateTime.Now.ToString());
            return DateTime.Now.ToString();
        }
        [HttpPost]
        public string GetClients()
        {
           
            return DateTime.Now.ToString();
        }
    }
}

using Microsoft.AspNetCore.SignalR;

namespace Dp.Server.Server.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string id, object option)
        {
            await Clients.All.SendAsync("ReceiveOption", id, option);
        }
    }
}

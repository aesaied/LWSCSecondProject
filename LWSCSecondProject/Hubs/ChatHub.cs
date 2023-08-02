using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using NuGet.Protocol.Plugins;

namespace LWSCSecondProject.Hubs
{
    //[Authorize]
    public class ChatHub :Hub
    {


        //MessageAll  :  method can called from  client 
        public async Task MessageAll(string sender, string message)
        {
            await Clients.All.SendAsync("NewMessage", sender, message);
        }


        public override async Task OnConnectedAsync()
        {
            await Clients.Others.SendAsync("NewMessage", "New User "+ Context.ConnectionId, "Connected...");

           await  base.OnConnectedAsync();
        }


        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}

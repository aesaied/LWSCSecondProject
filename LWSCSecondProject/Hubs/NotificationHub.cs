using Microsoft.AspNetCore.SignalR;

namespace LWSCSecondProject.Hubs
{
    public class NotificationHub :Hub
    {


        public async Task Notify(string msg)
        {


            await Clients.AllExcept(Context.ConnectionId).SendAsync("OnNotify",msg);
        } 
    }
}

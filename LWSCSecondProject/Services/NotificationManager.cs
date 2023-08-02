using LWSCSecondProject.Entities;
using LWSCSecondProject.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace LWSCSecondProject.Services
{
    public class NotificationManager : INotificationManager
    {

        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationManager(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task Notify(string msg)
        {
            await _hubContext.Clients.All.SendAsync("OnNotify", msg);

        }
    }
}

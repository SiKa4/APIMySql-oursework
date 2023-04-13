using APIMySqlСoursework.Controllers;
using APIMySqlСoursework.Model;
using Microsoft.AspNetCore.SignalR;

namespace APIMySqlСoursework.Hubs
{
    public class SignalRHubShedules : Hub
    {
        public void BroadcastShedules(SheduleClassesAndTypes shedule)
        {
            Clients.All.SendAsync("GetShedules", shedule);
        }
    }
}

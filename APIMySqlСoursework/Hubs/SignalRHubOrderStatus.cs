using APIMySqlСoursework.Controllers;
using APIMySqlСoursework.Model;
using Microsoft.AspNetCore.SignalR;
using Yandex.Checkout.V3;

namespace APIMySqlСoursework.Hubs
{
    public class SignalRHubOrderStatus : Hub
    {
        public void CurrentStatus(ShopOrderFullInfo order)
        {
            Clients.All.SendAsync("GetStatus", order);
        }
    }
}

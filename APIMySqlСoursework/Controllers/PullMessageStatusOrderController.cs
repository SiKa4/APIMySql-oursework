using APIMySqlСoursework.DBMySql;
using APIMySqlСoursework.Hubs;
using APIMySqlСoursework.Query;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Microsoft.AspNetCore.SignalR;
using APIMySqlСoursework.Model;
using System.Net;

namespace APIMySqlСoursework.Controllers
{
    [Route("api/pullMessages")]
    public class PullMessageStatusOrderController
    {
        private readonly IHubContext<SignalRHubOrderStatus> _hubContext;
        public DBConnection Db { get; }
        public PullMessageStatusOrderController(DBConnection db, IHubContext<SignalRHubOrderStatus> hubContext)
        {
            Db = db;
            _hubContext = hubContext;
        }

        [HttpPost]
        public async Task MessageStatus([FromBody] dynamic pay)
        {
            JsonDocument document = JsonDocument.Parse(Convert.ToString(pay));
            JsonElement idElement = document.RootElement.GetProperty("object").GetProperty("id");
            string idValue = idElement.GetString();
            JsonElement statusElement = document.RootElement.GetProperty("object").GetProperty("status");
            string statusValue = statusElement.GetString();
            await Db.Connection.OpenAsync();
            var statusDate = new OrderStatusDate(Db);
            var query = new ShopOrderQuery(Db);
            var order = await query.FindOrderByPaymentIdAsync(idValue);
            if(order != null)
            {
                switch (statusValue)
                {
                    case "succeeded":
                        statusDate.OrderStatus_id = 1;
                        break;
                    case "canceled":
                        statusDate.OrderStatus_id = 4;
                        break;
                    case "waiting_for_capture":
                        statusDate.OrderStatus_id = 2;
                        break;
                }
                statusDate.DateOrder = DateTime.Now;
                statusDate.ShopOrder_id = order.id_Order;
                await statusDate.InsertAsync();
                var orderFullInfo = await query.FindAllFullInfoByOrderIdAsync(order.id_Order);
                var orderStatusQuery = new OrderStatusDateQuery(Db);
                orderFullInfo.StatusAndDates = (await orderStatusQuery.FindAllAsync(order.id_Order)).OrderByDescending(x => x.DateOrder).ToList();
                var userSession = new SessionQuery(Db);
                var session = await userSession.FindOneAsync(order.User_id);
                await _hubContext.Clients.All.SendAsync("GetStatus", orderFullInfo);
            }
        }
    }
}

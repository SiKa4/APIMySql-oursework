using APIMySqlСoursework.DBMySql;
using APIMySqlСoursework.Query;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace APIMySqlСoursework.Controllers
{
    [Route("api/pullMessages")]
    public class PullMessageStatusOrderController
    {

        public DBConnection Db { get; }
        public PullMessageStatusOrderController(DBConnection db)
        {
            Db = db;
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
            var query = new ShopOrderQuery(Db);
            var order = await query.FindOrderByPaymentIdAsync(idValue);
            if(order != null)
            {
                switch (statusValue)
                {
                    case "succeeded":
                        order.OrderStatus_id = 1;
                        await order.UpdateAsync();
                        break;
                    case "canceled":
                        order.OrderStatus_id = 4;
                        await order.UpdateAsync();
                        break;
                    case "waiting_for_capture":
                        order.OrderStatus_id = 2;
                        await order.UpdateAsync();
                        break;
                }
            }
            
        }
    }
}

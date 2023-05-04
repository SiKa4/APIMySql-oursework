using APIMySqlСoursework.Attributes;
using APIMySqlСoursework.DBMySql;
using APIMySqlСoursework.Model;
using APIMySqlСoursework.Query;
using Microsoft.AspNetCore.Mvc;

namespace APIMySqlСoursework.Controllers
{
    [ApiKey]
    [Route("api/shopOrders")]
    public class ShopOrderController
    {
        public DBConnection Db { get; }
        public ShopOrderController(DBConnection db)
        {
            Db = db;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new ShopOrderQuery(Db);
            var result = await query.FindOneFullInfoAsync(id);
            if (result is null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new ShopOrderQuery(Db);
            var result = await query.FindAllFullInfoAsync(id);
            if (result is null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] List<ShopBasketFullInfo> body)
        {
            await Db.Connection.OpenAsync();
            var shopOrder = new ShopOrder(Db) {
                User_id = body[0].User_id
            };
            await shopOrder.InsertAsync();
            foreach(var i in body)
            {
                i.Db = Db;
                i.Order_id = shopOrder.id_Order;
                await i.UpdateOrderIdAsync();
            }
            return new OkObjectResult(body);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutOne(int id, [FromBody] ShopOrder body)
        {
            await Db.Connection.OpenAsync();
            var query = new ShopOrderQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            result.OrderStatus_id = body.OrderStatus_id;
            result.User_id = body.User_id;
            await result.UpdateAsync();
            return new OkObjectResult(await query.FindOneFullInfoAsync(result.id_Order));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new ShopOrderQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            var temp = await query.FindOneFullInfoAsync(result.id_Order);
            await result.DeleteAsync();
            return new OkObjectResult(temp);
        }
    }
}

using APIMySqlСoursework.Attributes;
using APIMySqlСoursework.DBMySql;
using APIMySqlСoursework.Model;
using APIMySqlСoursework.Query;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

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
        public async Task<IActionResult> GetAllByUserId(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new ShopOrderQuery(Db);
            var result = await query.FindAllFullInfoAsync(id);
            if (result is null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Post(int id, [FromBody]List<ClassInt> idsShopBasket)
        {
            await Db.Connection.OpenAsync();
            var body = new ShopOrder() { User_id = id };
            body.Db = Db;
            await body.InsertAsync();
            var query = new ShopOrderQuery(Db);
            var answer = await query.FindAllIdsAsync(idsShopBasket, body.id_Order);
            return new OkObjectResult(answer);
        }

    }

    public class ClassInt
    {
        public int idShopBasket { get; set; }
    }
}

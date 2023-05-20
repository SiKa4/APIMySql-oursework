using APIMySqlСoursework.Attributes;
using APIMySqlСoursework.DBMySql;
using APIMySqlСoursework.Model;
using APIMySqlСoursework.Query;
using Microsoft.AspNetCore.Mvc;

namespace APIMySqlСoursework.Controllers
{
    [ApiKey]
    [Route("api/shopBasket")]
    public class ShopBascetController
    {
        public DBConnection Db { get; }
        public ShopBascetController(DBConnection db)
        {
            Db = db;
        }//Обновление кол-ва товара по id товара и id usera только количество

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetOneByIdShopBasket(int id)
        //{
        //    await Db.Connection.OpenAsync();
        //    var query = new ShopBasketQuery(Db);
        //    var result = await query.FindOneAsync(id);
        //    if (result is null)
        //        return new NotFoundResult();
        //    return new OkObjectResult(result);
        //}

        [HttpGet("userIdFullInfo/{id}")]
        public async Task<IActionResult> GetAllUserIdFullInfo(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new ShopBasketQuery(Db);
            var result = await query.FindAllFullInfoByUserIdShopBusketAsync(id);
            if (result is null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }

        [HttpGet("userId/{id}")]
        public async Task<IActionResult> GetAllUserId(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new ShopBasketQuery(Db);
            var result = await query.FindAllUserIdAsync(id);
            if (result is null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ShopBasket body)
        {
            await Db.Connection.OpenAsync();
            body.Db = Db;
            var query = new ShopBasketQuery(Db);
            var answer = await query.FindOneAsync(body.ShopItem_id, body.User_id);
            if (answer == null)
            {
                await body.InsertAsync();
                return new OkObjectResult(await query.FindAllFullInfoByIdShopBusketAsync(body.id_ShopBasket));
            }
            else
            {
                answer.ShopItemCount += body.ShopItemCount - answer.ShopItemCount;
                await answer.UpdateAsync();
                return new OkObjectResult(await query.FindAllFullInfoByIdShopBusketAsync(answer.id_ShopBasket));
            }
        }
/*
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOne(int id, [FromBody] ShopBasket body)
        {
            await Db.Connection.OpenAsync();
            var query = new ShopBasketQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            result.ShopItem_id = body.ShopItem_id;
            result.User_id = body.User_id;
            result.ShopItemCount = body.ShopItemCount;
            await result.UpdateAsync();
            return new OkObjectResult(query.FindAllFullInfoByIdShopBusketAsync(result.id_ShopBasket));
        }
*/
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new ShopBasketQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            var answer = await query.FindAllFullInfoByIdShopBusketAsync(result.id_ShopBasket);
            await result.DeleteAsync();
            return new OkObjectResult(answer);
        }
    }
}

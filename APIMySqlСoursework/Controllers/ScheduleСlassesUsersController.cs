using APIMySqlСoursework.Attributes;
using APIMySqlСoursework.DBMySql;
using APIMySqlСoursework.Model;
using APIMySqlСoursework.Query;
using Microsoft.AspNetCore.Mvc;

namespace APIMySqlСoursework.Controllers
{
    [ApiKey]
    [Route("api/scheduleСlassesUsers")]
    public class ScheduleСlassesUsersController : ControllerBase
    {
        public DBConnection Db { get; }
        public ScheduleСlassesUsersController(DBConnection db)
        {
            Db = db;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAll(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new ScheduleСlassesUsersQuery(Db);
            var result = await query.FindAllAsync(id);
            if (result is null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ScheduleСlassesUsers body)
        {
            await Db.Connection.OpenAsync();
            body.Db = Db;
            await body.InsertAsync();
            return new OkObjectResult(body);
        }

        [HttpPut]
        public async Task<IActionResult> PutOne([FromBody] ScheduleСlassesUsers body)
        {
            await Db.Connection.OpenAsync();
            var query = new ScheduleСlassesUsersQuery(Db);
            var result = await query.FindOneAsync(body.ScheduleСlass_id, body.User_id);
            if (result is null)
                return new NotFoundResult();
            result.isActive = body.isActive;
            await result.UpdateAsync();
            return new OkObjectResult(result);
        }

        [HttpDelete("{ScheduleСlass_id}/{User_id}")]
        public async Task<IActionResult> DeleteOne(int ScheduleСlass_id, int User_id)
        {
            await Db.Connection.OpenAsync();
            var query = new ScheduleСlassesUsersQuery(Db);
            var result = await query.FindOneAsync(ScheduleСlass_id, User_id);
            if (result is null)
                return new NotFoundResult();
            await result.DeleteAsync();
            return new OkObjectResult(result);
        }
    }
}

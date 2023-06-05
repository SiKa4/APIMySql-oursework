using APIMySqlСoursework.Attributes;
using APIMySqlСoursework.DBMySql;
using APIMySqlСoursework.Model;
using APIMySqlСoursework.Query;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;
using System.Net;
using System.Security.Claims;

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
            var a = CrossControllerSession.Session.GetString("login");
            return new OkObjectResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ScheduleСlassesUsers body)
        {
            await Db.Connection.OpenAsync();
            body.Db = Db;
            var query = new ScheduleСlassesUsersQuery(Db);
            ScheduleСlassesUsers temp = await query.FindOneAsync(body.ScheduleСlass_id, body.User_id);
            var querySchedule = new SheduleClassesAndTypesQuery(Db);
            var tempShedule = await querySchedule.FindOneAsync(body.ScheduleСlass_id);
            var currentPeople = await query.FindOneAsyncIdSchedule(body.ScheduleСlass_id);
            if (temp is null && currentPeople < tempShedule.MaxOfPeople)
            {
                await body.InsertAsync();
                ScheduleСlassesUsersFullInfo answer = await query.FindAllAsyncIdUserAndIdSchedule(body.User_id, body.ScheduleСlass_id);
                return new OkObjectResult(answer);
            }
            else if(currentPeople < tempShedule.MaxOfPeople)
            {
                temp.isActive = true;
                await temp.UpdateAsync();
                ScheduleСlassesUsersFullInfo answer = await query.FindAllAsyncIdUserAndIdSchedule(temp.User_id, temp.ScheduleСlass_id);
                return new OkObjectResult(answer);
            }
            return new OkObjectResult(null);
        }

        [HttpPut]
        public async Task<IActionResult> PutOne([FromBody]ScheduleСlassesUsers body)
        {
            await Db.Connection.OpenAsync();
            var query = new ScheduleСlassesUsersQuery(Db);
            var result = await query.FindOneAsync(body.ScheduleСlass_id, body.User_id);
            if (result is null)
                return new NotFoundResult();
            result.isActive = body.isActive;
            await result.UpdateAsync();
            return new OkObjectResult(await query.FindAllAsyncIdUserAndIdSchedule(result.User_id, result.ScheduleСlass_id));
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

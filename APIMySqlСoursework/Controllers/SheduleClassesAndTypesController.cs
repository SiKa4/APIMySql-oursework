using APIMySqlСoursework.Attributes;
using APIMySqlСoursework.DBMySql;
using APIMySqlСoursework.Hubs;
using APIMySqlСoursework.Model;
using APIMySqlСoursework.Query;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace APIMySqlСoursework.Controllers
{
    //[ApiKey]
    [Route("api/shedules")]
    public class SheduleClassesAndTypesController : ControllerBase
    {
        private readonly IHubContext<SignalRHubShedules> _hubContext;
        public DBConnection Db { get; }
        public SheduleClassesAndTypesController(DBConnection db, IHubContext<SignalRHubShedules> hubContext)
        {
            Db = db;
            _hubContext = hubContext;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new SheduleClassesAndTypesQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            await Db.Connection.OpenAsync();
            var query = new SheduleClassesAndTypesQuery(Db);
            var result = await query.FindAllAsync();
            if (result is null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SheduleClassesAndTypes body)
        {
            await Db.Connection.OpenAsync();
            body.Db = Db;
            await body.InsertAsync();
            await _hubContext.Clients.All.SendAsync("GetShedules", body);
            return new OkObjectResult(body);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutOne(int id, [FromBody] SheduleClassesAndTypes body)
        {
            await Db.Connection.OpenAsync();
            var query = new SheduleClassesAndTypesQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            result.id_ScheduleСlass = body.id_ScheduleСlass;
            result.Location = body.Location;
            result.TimeStart = body.TimeStart;
            result.TimeEnd = body.TimeEnd;
            result.MaxOfPeople = body.MaxOfPeople;
            result.ScheduleClassType_id = body.ScheduleClassType_id;
            result.Teacher_id = body.Teacher_id;
            await result.UpdateAsync();
            await _hubContext.Clients.All.SendAsync("GetShedules", result);
            return new OkObjectResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new SheduleClassesAndTypesQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            await result.DeleteAsync();
            return new OkResult();
        }
    }
}

using APIMySqlСoursework.DBMySql;
using APIMySqlСoursework.Hubs;
using APIMySqlСoursework.Model;
using APIMySqlСoursework.Query;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace APIMySqlСoursework.Controllers
{
    [Route("api/individualShedules")]
    public class IndividualShedulessClassController : Controller
    {
        private readonly IHubContext<SignalRHubShedules> _hubContext;
        public DBConnection Db { get; }
        public IndividualShedulessClassController(DBConnection db, IHubContext<SignalRHubShedules> hubContext)
        {
            Db = db;
            _hubContext = hubContext;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new IndividualScheduleСlassesQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }

        [HttpGet("teacherId/{id}")]
        public async Task<IActionResult> GetOneTeacherId(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new IndividualScheduleСlassesQuery(Db);
            var result = await query.FindAllFullInfoByIdAsync(id, "Teacher_id");
            if (result is null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }

        [HttpGet("userId/{id}")]
        public async Task<IActionResult> GetOneUserId(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new IndividualScheduleСlassesQuery(Db);
            var result = await query.FindAllFullInfoByIdAsync(id, "User_id");
            if (result is null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            await Db.Connection.OpenAsync();
            var query = new IndividualScheduleСlassesQuery(Db);
            var result = await query.FindAllFullInfoAsync();
            if (result is null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] IndividualShedulessClasses body)
        {
            await Db.Connection.OpenAsync();
            body.Db = Db;
            await body.InsertAsync();
            return new OkObjectResult(body);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutOne(int id, [FromBody] IndividualShedulessClasses body)
        {
            await Db.Connection.OpenAsync();
            var query = new IndividualScheduleСlassesQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            result.TimeStart = body.TimeStart;
            result.TimeEnd = body.TimeEnd;
            result.Teacher_id = body.Teacher_id;
            result.User_id = body.User_id;
            result.SheduleClasseType_id = body.SheduleClasseType_id;
            result.isActive = body.isActive;
            await result.UpdateAsync();
            return new OkObjectResult(query.FindAllFullInfoByIdIndividualScheduleAsync(result.id_IndividualScheduleСlass));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new IndividualScheduleСlassesQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            var temp = await query.FindAllFullInfoByIdIndividualScheduleAsync(result.id_IndividualScheduleСlass);
            await result.DeleteAsync();
            temp.isDelete = true;
            return new OkObjectResult(temp);
        }


    }
}

using APIMySqlСoursework.Attributes;
using APIMySqlСoursework.DBMySql;
using APIMySqlСoursework.Model;
using APIMySqlСoursework.Query;
using Microsoft.AspNetCore.Mvc;

namespace APIMySqlСoursework.Controllers
{
    [ApiKey]
    [Route("api/coachRaiting")]
    public class CoachRaitingController
    {
        public DBConnection Db { get; }
        public CoachRaitingController(DBConnection db)
        {
            Db = db;
        }

        [HttpGet("coachId/{id}")]
        public async Task<IActionResult> GetAllByCoachIdFullInfo(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new CoachRaitingQuery(Db);
            var result = await query.FindOneByCoachIdFullInfoAsync(id);
            if (result is null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFullInfo()
        {
            await Db.Connection.OpenAsync();
            var query = new CoachRaitingQuery(Db);
            var result = await query.FindAllFullInfoAsync();
            if (result is null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }
    }
}

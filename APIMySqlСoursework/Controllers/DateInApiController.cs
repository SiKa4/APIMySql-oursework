using APIMySqlСoursework.Attributes;
using APIMySqlСoursework.DBMySql;
using APIMySqlСoursework.Query;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Text.Json.Serialization;

namespace APIMySqlСoursework.Controllers
{
    [ApiKey]
    [Route("api/dateAPI")]
    public class DateInApiController : ControllerBase
    {
        public DBConnection Db { get; }
        public DateInApiController(DBConnection db)
        {
            Db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            await Db.Connection.OpenAsync();
            var result = await GetDate();
            if (result is null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }

        private async Task<List<DateInApi>> GetDate()
        {
            DateTime dateTime = DateTime.Now;
            List<DateInApi> list = new List<DateInApi>();
            for (int i = 0; i < 7; i++)
            {
                if (dateTime.DayOfWeek != DayOfWeek.Monday)
                {
                    dateTime = dateTime.AddDays(-1);
                }
                else break;
            }
            for (int i = 0; i < 7; i++)
            {
                var temp = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
                list.Add(new DateInApi(dateTime));
                dateTime = dateTime.AddDays(1);
            }
            return list;
        }
    }
}

class DateInApi{
    public DateTime date { get; set; }
    [JsonConstructor]
    public DateInApi() { }

    public DateInApi(DateTime date)
    {
        this.date = date;
    }
}

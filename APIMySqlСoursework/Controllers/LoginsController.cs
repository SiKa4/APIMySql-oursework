using APIMySqlСoursework.Attributes;
using APIMySqlСoursework.DBMySql;
using APIMySqlСoursework.Model;
using APIMySqlСoursework.Query;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Security.Claims;

namespace APIMySqlСoursework.Controllers
{
    [ApiKey]
    [Route("api/logins")]
    public class LoginsController : HomeController
    {
        public DBConnection Db { get; }
        public LoginsController(DBConnection db)
        {
            Db = db;
        }

        [HttpPost("logPass")]
        public async Task<IActionResult> GetOneByLoginPassword([FromBody]LogPass logPass)
        {     
            await Db.Connection.OpenAsync();
            var query = new LoginsQuery(Db);
            var result = await query.FindOneAsyncLoginPassword(logPass.Login, logPass.Password);
            if (result is null)
                return new NotFoundResult();
            CrossControllerSession.Session.SetString("login", $"{result.Login}");
            //HttpContext.Session.SetString("login", $"{result.Login}");
            return new OkObjectResult(result);
        }

        [HttpGet("{login}")]
        public async Task<IActionResult> GetOneByLogin(string login)
        {
            await Db.Connection.OpenAsync();
            var query = new LoginsQuery(Db);
            var result = await query.FindOneAsyncLogin(login);
            if (result is null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }

        [HttpGet("logId/{id}")]
        public async Task<IActionResult> GetOneByLogin(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new LoginsQuery(Db);
            var result = await query.FindOneAsyncUserId(id);
            if (result is null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            await Db.Connection.OpenAsync();
            var query = new LoginsQuery(Db);
            var result = await query.FindAllAsync();
            if (result is null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Logins body)
        {
            await Db.Connection.OpenAsync();
            body.Db = Db;
            await body.InsertAsync();
            return new OkObjectResult(body);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutOne(int id, [FromBody] Logins body)
        {
            await Db.Connection.OpenAsync();
            var query = new LoginsQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            result.Login = body.Login;
            result.Password = body.Password;
            result.User_id = body.User_id;
            await result.UpdateAsync();
            return new OkObjectResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new LoginsQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            await result.DeleteAsync();
            return new OkResult();
        }
    }
}

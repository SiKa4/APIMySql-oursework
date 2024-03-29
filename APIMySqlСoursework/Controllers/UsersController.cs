﻿using APIMySqlСoursework.Attributes;
using APIMySqlСoursework.DBMySql;
using APIMySqlСoursework.Model;
using APIMySqlСoursework.Query;
using Microsoft.AspNetCore.Mvc;

namespace APIMySqlСoursework.Controllers
{
    [ApiKey]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        public DBConnection Db { get; }
        public UsersController(DBConnection db)
        {
            Db = db;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new UsersQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            await Db.Connection.OpenAsync();
            var query = new UsersQuery(Db);
            var result = await query.FindAllAsync();
            if (result is null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }

        [HttpGet("coach")]
        public async Task<IActionResult> GetAllCoach()
        {
            await Db.Connection.OpenAsync();
            var query = new UsersQuery(Db);
            var result = await query.FindAllCoachAsync();
            if (result is null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }

        //[HttpGet("/getTrainer")]
        //public async Task<IActionResult> GetAllTrainer()
        //{

        //}
        //влкючи постман
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Users body)
        {
            await Db.Connection.OpenAsync();
            body.Db = Db;
            await body.InsertAsync();
            return new OkObjectResult(body);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutOne(int id, [FromBody] Users body)
        {
            await Db.Connection.OpenAsync();
            var query = new UsersQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            result.FullName = body.FullName;
            result.Role_id = body.Role_id;
            result.Number = body.Number;
            await result.UpdateAsync();
            return new OkObjectResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new UsersQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            await result.DeleteAsync();
            return new OkResult();
        }
    }
}

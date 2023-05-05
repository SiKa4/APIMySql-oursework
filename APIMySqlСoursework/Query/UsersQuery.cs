using APIMySqlСoursework.DBMySql;
using APIMySqlСoursework.Model;
using Microsoft.AspNet.SignalR.Hubs;
using MySqlConnector;
using System.Data;
using System.Data.Common;

namespace APIMySqlСoursework.Query
{
    public class UsersQuery
    {
        public DBConnection Db { get; }

        public UsersQuery(DBConnection db)
        {
            Db = db;
        }

        public async Task<Users> FindOneAsync(int id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = $"SELECT * FROM Users u JOIN Roles r ON u.Role_id = r.id_Role WHERE id_User = {id}";
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }
        public async Task<List<Coach>> FindAllCoachAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = $"SELECT * FROM Users u JOIN CoachInfo c ON u.id_User = c.User_id JOIN Logins l ON l.User_id = u.id_User JOIN Roles r ON u.Role_id = r.id_Role WHERE Role_id = 2;";
            var result = await ReadAllCoachAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result : null;
        }

        public async Task<List<Users>> FindAllAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = $"SELECT * FROM Users u JOIN Roles r ON u.Role_id = r.id_Role";
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result : null;
        }

        private async Task<List<Coach>> ReadAllCoachAsync(DbDataReader reader)
        {
            var users = new List<Coach>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new Coach()
                    {
                        id_User = reader.GetInt32(0),
                        FullName = reader.GetString(1),
                        Role_id = reader.GetInt32(2),
                        Number = reader.GetString(3),
                        Description = reader.GetString(5),
                        Image_URL = reader.GetString(6),
                        Email = reader.GetString(9),
                    };
                    /////////////////////////////////////////созздать метод подсчета 
                    users.Add(post);
                }
            }
            return users;
        }

        

        private async Task<List<Users>> ReadAllAsync(DbDataReader reader)
        {
            var users = new List<Users>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new Users(Db)
                    {
                        id_User = reader.GetInt32(0),
                        FullName = reader.GetString(1),
                        Role_id = reader.GetInt32(2),
                        Number = reader.GetString(3),
                        Role_Name = reader.GetString(5),
                    };
                    users.Add(post);
                }
            }
            return users;
        }
    }
}

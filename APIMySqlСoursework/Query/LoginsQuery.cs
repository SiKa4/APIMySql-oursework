using APIMySqlСoursework.DBMySql;
using APIMySqlСoursework.Model;
using System.Data.Common;

namespace APIMySqlСoursework.Query
{
    public class LoginsQuery
    {
        public DBConnection Db { get; }

        public LoginsQuery(DBConnection db)
        {
            Db = db;
        }

        public async Task<Logins> FindOneAsyncLoginPassword(string login, string password)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = $"SELECT * FROM Logins WHERE Login = '{login}' AND Password = '{password}'";
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }

        public async Task<Logins> FindOneAsyncLogin(string login)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = $"SELECT * FROM Logins WHERE Login = '{login}'";
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }

        public async Task<Logins> FindOneAsync(int id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = $"SELECT * FROM Logins WHERE id_Login = {id}";
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }

        public async Task<Logins> FindOneAsyncUserId(int id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = $"SELECT * FROM Logins WHERE User_id = {id}";
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }

        public async Task<List<Logins>> FindAllAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = $"SELECT * FROM Logins";
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result : null;
        }

        private async Task<List<Logins>> ReadAllAsync(DbDataReader reader)
        {
            var posts = new List<Logins>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new Logins(Db)
                    {
                        id_Login = reader.GetInt32(0),
                        Login = reader.GetString(1),
                        Password = reader.GetString(2),
                        User_id = reader.GetInt32(3),
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }
    }
}

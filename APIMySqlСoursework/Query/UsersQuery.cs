using APIMySqlСoursework.DBMySql;
using APIMySqlСoursework.Model;
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
            cmd.CommandText = $"SELECT * FROM Users WHERE id_User = {id}";
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }


        public async Task<List<Users>> FindAllAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = $"SELECT * FROM Users";
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result : null;
        }

        private async Task<List<Users>> ReadAllAsync(DbDataReader reader)
        {
            var posts = new List<Users>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new Users(Db)
                    {
                        id_User = reader.GetInt32(0),
                        FullName = reader.GetString(1),
                        Role_id = reader.GetInt32(2),
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }
    }
}

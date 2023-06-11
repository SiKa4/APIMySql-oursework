using APIMySqlСoursework.DBMySql;
using APIMySqlСoursework.Model;
using System.Data.Common;

namespace APIMySqlСoursework.Query
{
    public class SessionQuery
    {
        public DBConnection Db { get; }

        public SessionQuery(DBConnection db)
        {
            Db = db;
        }

        public async Task<Sessions> FindOneAsync(int idUser)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = $"SELECT * FROM Sessions WHERE User_id = {idUser}";
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result;
        }

        private async Task<Sessions> ReadAllAsync(DbDataReader reader)
        {
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var session = new Sessions(Db)
                    {
                        id_Session = reader.GetInt32(0),
                        User_id = reader.GetInt32(1),
                        UserIP = reader.GetString(2),
                        LastAuthorization = reader.GetDateTime(3),
                    };
                    return session;
                }
            }
            return null;
        }
    }
}

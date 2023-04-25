using APIMySqlСoursework.DBMySql;
using APIMySqlСoursework.Model;
using System.Data.Common;

namespace APIMySqlСoursework.Query
{
    public class ScheduleСlassesUsersQuery
    {
        public DBConnection Db { get; }
        public ScheduleСlassesUsersQuery(DBConnection db)
        {
            Db = db;
        }
        public async Task<ScheduleСlassesUsers> FindOneAsync(int idScheduleСlass, int idUser)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = $"SELECT * FROM ScheduleСlasses_Users WHERE ScheduleСlass_id = {idScheduleСlass} AND User_id = {idUser};";
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }

        public async Task<List<ScheduleСlassesUsers>> FindAllAsync(int userId)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = $"SELECT * FROM ScheduleСlasses_Users WHERE User_id = {userId}";
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result : null;
        }

        private async Task<List<ScheduleСlassesUsers>> ReadAllAsync(DbDataReader reader)
        {
            var scheduleСlassesUsers = new List<ScheduleСlassesUsers>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var scheduleСlasseUser = new ScheduleСlassesUsers(Db)
                    {
                        ScheduleСlass_id = reader.GetInt32(0),
                        User_id = reader.GetInt32(1),
                        RecordingTime = reader.GetDateTime(2),
                        isActive = reader.GetBoolean(3),
                    };
                    scheduleСlassesUsers.Add(scheduleСlasseUser);
                }
            }
            return scheduleСlassesUsers;
        }
    }
}

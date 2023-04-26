using APIMySqlСoursework.DBMySql;
using APIMySqlСoursework.Model;
using System.Data.Common;
using System.Diagnostics.Metrics;

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

        public async Task<List<ScheduleСlassesUsersFullInfo>> FindAllAsync(int idUser)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = $"SELECT su.ScheduleСlass_id, su.User_id, su.RecordingTime, s.Location, s.TimeStart, s.TimeEnd, s.MaxOfPeople, s.Teacher_id, u.FullName, st.Name, st.Details, st.Image, su.isActive FROM ScheduleСlasses_Users su JOIN ScheduleСlasses s ON s.id_ScheduleСlass = su.ScheduleСlass_id JOIN ScheduleClassesTypes st ON st.id_ScheduleClassType = s.ScheduleClassType_id JOIN Users u ON u.id_User = s.Teacher_id WHERE su.User_id = {idUser};";
            var result = await ReadAllAsyncFullInfo(await cmd.ExecuteReaderAsync());
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
                        isActive = reader.GetBoolean(3)
                    };
                    scheduleСlassesUsers.Add(scheduleСlasseUser);
                }
            }
            return scheduleСlassesUsers;
        }

        private async Task<List<ScheduleСlassesUsersFullInfo>> ReadAllAsyncFullInfo(DbDataReader reader)
        {
            var scheduleСlassesUsers = new List<ScheduleСlassesUsersFullInfo>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var scheduleСlasseUser = new ScheduleСlassesUsersFullInfo(Db)
                    {
                        ScheduleСlass_id = reader.GetInt32(0),
                        User_id = reader.GetInt32(1),
                        RecordingTime = reader.GetDateTime(2),
                        Location = reader.GetString(3),
                        TimeStart = reader.GetDateTime(4),
                        TimeEnd = reader.GetDateTime(5),
                        MaxOfPeople = reader.GetInt32(6),
                        Teacher_id = reader.GetInt32(7),
                        Teacher_FullName = reader.GetString(8),
                        Type_Name = reader.GetString(9),
                        Details = reader.GetString(10),
                        Image_Type = reader.GetString(11),
                        isActiveUser = reader.GetBoolean(12)
                    };
                    scheduleСlasseUser.isDelete = false;
                    if (scheduleСlasseUser.TimeStart <= DateTime.Now) scheduleСlasseUser.isActive = false;
                    else scheduleСlasseUser.isActive = true;

                    scheduleСlassesUsers.Add(scheduleСlasseUser);
                }
            }
            scheduleСlassesUsers = scheduleСlassesUsers.OrderBy(x => x.TimeStart).ToList();
            return scheduleСlassesUsers;
        }
    }
}

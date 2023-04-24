using APIMySqlСoursework.DBMySql;
using APIMySqlСoursework.Model;
using System.Data.Common;

namespace APIMySqlСoursework.Query
{
    public class SheduleClassesAndTypesQuery
    {
        public DBConnection Db { get; }

        public SheduleClassesAndTypesQuery(DBConnection db)
        {
            Db = db;
        }

        public async Task<SheduleClassesAndTypes> FindOneAsync(int id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = $"SELECT * FROM ScheduleСlasses s JOIN ScheduleClassesTypes t ON t.id_ScheduleClassType = s.ScheduleClassType_id JOIN Users u ON u.id_User = s.Teacher_id WHERE s.id_ScheduleСlass = {id}";
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }


        public async Task<List<SheduleClassesAndTypes>> FindAllAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = $"SELECT * FROM ScheduleСlasses s JOIN ScheduleClassesTypes t ON t.id_ScheduleClassType = s.ScheduleClassType_id JOIN Users u ON u.id_User = s.Teacher_id";
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result : null;
        }

        private async Task<List<SheduleClassesAndTypes>> ReadAllAsync(DbDataReader reader)
        {
            var sheduleClassesAndTypes = new List<SheduleClassesAndTypes>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var schedule = new SheduleClassesAndTypes(Db)
                    {
                        id_ScheduleСlass = reader.GetInt32(0),
                        Location = reader.GetString(1),
                        TimeStart = reader.GetDateTime(2),
                        TimeEnd = reader.GetDateTime(3),
                        MaxOfPeople = reader.GetInt32(4),
                        ScheduleClassType_id = reader.GetInt32(5),
                        Teacher_id = reader.GetInt32(6),
                        Teacher_FullName = reader.GetString(12),
                        Type_Name = reader.GetString(8),
                        Details = reader.GetString(9),
                        Image_Type = reader.GetString(10),
                    };
                    //dopisatb/
                    sheduleClassesAndTypes.Add(schedule);
                }
            }
            return sheduleClassesAndTypes;
        }
    }
}

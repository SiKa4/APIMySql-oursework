using APIMySqlСoursework.DBMySql;
using APIMySqlСoursework.Model;
using System.Data.Common;

namespace APIMySqlСoursework.Query
{
    public class IndividualScheduleСlassesQuery
    {
        public DBConnection Db { get; }

        public IndividualScheduleСlassesQuery(DBConnection db)
        {
            Db = db;
        }

        public async Task<IndividualShedulessClasses> FindOneAsync(int id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = $"SELECT id_IndividualScheduleСlass, User_id, Teacher_id, TimeStart, TimeEnd, isActive, SheduleClasseType_id FROM IndividualScheduleСlasses WHERE id_IndividualScheduleСlass = {id};";
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }

        public async Task<IndividualShedulessClassesFullInfo> FindAllFullInfoByIdIndividualScheduleAsync(int id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = $"SELECT s.id_IndividualScheduleСlass, s.User_id, s.Teacher_id, s.TimeStart, s.TimeEnd, s.isActive, us.FullName, u.FullName, t.Name, t.Details, t.Image FROM IndividualScheduleСlasses s JOIN ScheduleClassesTypes t ON t.id_ScheduleClassType = s.SheduleClasseType_id JOIN Users u ON u.id_User = s.Teacher_id JOIN Users us ON us.id_User = s.User_id WHERE id_IndividualScheduleСlass = {id};";
            var result = await ReadAllAsyncFullInfo(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }

        public async Task<List<IndividualShedulessClassesFullInfo>> FindAllFullInfoAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = $"SELECT s.id_IndividualScheduleСlass, s.User_id, s.Teacher_id, s.TimeStart, s.TimeEnd, s.isActive, us.FullName, u.FullName, t.Name, t.Details, t.Image FROM IndividualScheduleСlasses s JOIN ScheduleClassesTypes t ON t.id_ScheduleClassType = s.SheduleClasseType_id JOIN Users u ON u.id_User = s.Teacher_id JOIN Users us ON us.id_User = s.User_id;";
            var result = await ReadAllAsyncFullInfo(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result : null;
        }

        public async Task<List<IndividualShedulessClassesFullInfo>> FindAllFullInfoByIdAsync(int id, string name)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = $"SELECT s.id_IndividualScheduleСlass, s.User_id, s.Teacher_id, s.TimeStart, s.TimeEnd, s.isActive, us.FullName, u.FullName, t.Name, t.Details, t.Image FROM IndividualScheduleСlasses s JOIN ScheduleClassesTypes t ON t.id_ScheduleClassType = s.SheduleClasseType_id JOIN Users u ON u.id_User = s.Teacher_id JOIN Users us ON us.id_User = s.User_id WHERE s.{name} = {id};";
            var result = await ReadAllAsyncFullInfo(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result : null;
        }

        private async Task<List<IndividualShedulessClasses>> ReadAllAsync(DbDataReader reader)
        {
            var IndividualShedulessClasses = new List<IndividualShedulessClasses>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var IndividualShedulessClass = new IndividualShedulessClasses(Db)
                    {
                        id_IndividualScheduleСlass = reader.GetInt32(0),
                        User_id = reader.GetInt32(1),
                        Teacher_id = reader.GetInt32(2),
                        TimeStart = reader.GetDateTime(3),
                        TimeEnd = reader.GetDateTime(4),
                        isActive = reader.GetBoolean(5),
                        SheduleClasseType_id = reader.GetInt32(6),
                        
                    };
                    IndividualShedulessClasses.Add(IndividualShedulessClass);
                }
            }
            return IndividualShedulessClasses;
        }

        private async Task<List<IndividualShedulessClassesFullInfo>> ReadAllAsyncFullInfo(DbDataReader reader)
        {
            var IndividualShedulessClasses = new List<IndividualShedulessClassesFullInfo>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var IndividualShedulessClass = new IndividualShedulessClassesFullInfo(Db)
                    {
                        id_IndividualScheduleСlass = reader.GetInt32(0),
                        User_id = reader.GetInt32(1),
                        Teacher_id = reader.GetInt32(2),
                        TimeStart = reader.GetDateTime(3),
                        TimeEnd = reader.GetDateTime(4),
                        isActiveUser = reader.GetBoolean(5),
                        User_Name = reader.GetString(6),
                        Teacher_Name = reader.GetString(7),
                        Type_Name = reader.GetString(8),
                        Details = reader.GetString(9),
                        Image_Type = reader.GetString(10),
                    };
                    IndividualShedulessClass.isDelete = false;
                    if (IndividualShedulessClass.TimeStart <= DateTime.Now) IndividualShedulessClass.isActive = false;
                    else IndividualShedulessClass.isActive = true;
                    IndividualShedulessClasses.Add(IndividualShedulessClass);
                }
            }
            return IndividualShedulessClasses;
        }
    }
}

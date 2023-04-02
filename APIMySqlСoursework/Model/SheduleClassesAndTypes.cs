using APIMySqlСoursework.DBMySql;
using MySqlConnector;
using System.Data;
using System.Text.Json.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace APIMySqlСoursework.Model
{
    public class SheduleClassesAndTypes
    {
        public int id_ScheduleСlass { get; set; }
        public string Location { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public int MaxOfPeople { get; set; }
        public int ScheduleClassType_id { get; set; }
        public int Teacher_id { get; set; }
        public string Teacher_FullName { get; set; }
        public string Type_Name { get; set; }
        public string Details { get; set; }
        public string Image_Type { get; set; }
        internal DBConnection Db { get; set; }

        [JsonConstructor]
        public SheduleClassesAndTypes()
        {
        }

        internal SheduleClassesAndTypes(DBConnection db)
        {
            Db = db;
        }

        public async Task InsertAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO `ScheduleСlasses` (`Location`, `TimeStart`, `TimeEnd`, `MaxOfPeople`,`ScheduleClassType_id`, `Teacher_id`) 
            VALUES (@Location, @TimeStart, @TimeEnd, @MaxOfPeople, @ScheduleClassType_id, @Teacher_id);";
            BindParams(cmd);
            await cmd.ExecuteNonQueryAsync();
            id_ScheduleСlass = (int)cmd.LastInsertedId;
        }

        public async Task DeleteAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM `ScheduleСlasses` WHERE `id_ScheduleСlass` = @id_ScheduleСlass;";
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task UpdateAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"UPDATE `ScheduleСlasses` SET `Location` = @Location, `TimeStart` = @TimeStart, `TimeEnd` = @TimeEnd, `MaxOfPeople` = @MaxOfPeople, `ScheduleClassType_id` = @ScheduleClassType_id, `Teacher_id` = @Teacher_id 
            WHERE `id_ScheduleClass` = @id_ScheduleClass;";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        private void BindId(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id_ScheduleСlass",
                DbType = DbType.Int32,
                Value = id_ScheduleСlass,
            });
        }

        private void BindParams(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@Location",
                DbType = DbType.String,
                Value = Location,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@TimeStart",
                DbType = DbType.DateTime,
                Value = TimeStart,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@TimeEnd",
                DbType = DbType.DateTime,
                Value = TimeEnd,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@MaxOfPeople",
                DbType = DbType.Int32,
                Value = MaxOfPeople,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@ScheduleClassType_id",
                DbType = DbType.Int32,
                Value = ScheduleClassType_id,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@Teacher_id",
                DbType = DbType.Int32,
                Value = Teacher_id,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@Teacher_FullName",
                DbType = DbType.String,
                Value = Teacher_FullName,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@Details",
                DbType = DbType.String,
                Value = Details,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@Image_Type",
                DbType = DbType.String,
                Value = Image_Type,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@Type_Name",
                DbType = DbType.String,
                Value = Type_Name,
            });
        }
    }
}

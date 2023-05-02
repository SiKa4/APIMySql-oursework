using APIMySqlСoursework.DBMySql;
using MySqlConnector;
using System.Data;
using System.Text.Json.Serialization;

namespace APIMySqlСoursework.Model
{
    public class IndividualShedulessClasses
    {
        public int id_IndividualScheduleСlass { get; set; }
        public int User_id { get; set; }
        public int Teacher_id { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public int SheduleClasseType_id { get; set; }
        public bool isActive { get; set; }

        internal DBConnection Db { get; set; }
        [JsonConstructor]
        public IndividualShedulessClasses()
        {

        }

        internal IndividualShedulessClasses(DBConnection db)
        {
            Db = db;
        }

        public async Task InsertAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO `IndividualScheduleСlasses` (`User_id`, `Teacher_id`,`TimeStart`, `TimeEnd`, `SheduleClasseType_id`, `isActive`) 
            VALUES (@User_id, @Teacher_id, @TimeStart, @TimeEnd, @SheduleClasseType_id, @isActive);";
            BindParams(cmd);
            await cmd.ExecuteNonQueryAsync();
            id_IndividualScheduleСlass = (int)cmd.LastInsertedId;
        }

        public async Task DeleteAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM IndividualScheduleСlasses WHERE id_IndividualScheduleСlass = @id_IndividualScheduleСlass;";
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task UpdateAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"UPDATE IndividualScheduleСlasses SET `isActive` = @isActive, `TimeStart` = @TimeStart, `TimeEnd` = @TimeEnd, `Teacher_id` = @Teacher_id 
            WHERE id_IndividualScheduleСlass = @id_IndividualScheduleСlass;";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        private void BindId(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id_IndividualScheduleСlass",
                DbType = DbType.Int32,
                Value = id_IndividualScheduleСlass,
            });
        }

        private void BindParams(MySqlCommand cmd)
        {
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
                ParameterName = "@Teacher_id",
                DbType = DbType.Int32,
                Value = Teacher_id,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@User_id",
                DbType = DbType.Int32,
                Value = User_id,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@SheduleClasseType_id",
                DbType = DbType.Int32,
                Value = SheduleClasseType_id,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@isActive",
                DbType = DbType.Int32,
                Value = isActive,
            });
        }


    }

    public class IndividualShedulessClassesFullInfo
    {
        public int id_IndividualScheduleСlass { get; set; }
        public int User_id { get; set; }
        public int Teacher_id { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public string Teacher_Name { get; set; }
        public string User_Name { get; set; }
        public string Type_Name { get; set; }
        public string Details { get; set; }
        public string Image_Type { get; set; }
        public bool isActiveUser { get; set; }
        public bool isActive { get; set; }
        public bool isDelete { get; set; }
        internal DBConnection Db { get; set; }
        [JsonConstructor]
        public IndividualShedulessClassesFullInfo()
        {

        }

        internal IndividualShedulessClassesFullInfo(DBConnection db)
        {
            Db = db;
        }
    }
}

using APIMySqlСoursework.DBMySql;
using MySqlConnector;
using System.Data;
using System.Text.Json.Serialization;

namespace APIMySqlСoursework.Model
{
    public class ScheduleСlassesUsers
    {
        public int ScheduleСlass_id { get; set; }
        public int User_id { get; set; }
        public DateTime RecordingTime { get; set; }

        public bool isActive { get; set; }
        internal DBConnection Db { get; set; }

        [JsonConstructor]
        public ScheduleСlassesUsers()
        {
        }

        internal ScheduleСlassesUsers(DBConnection db)
        {
            Db = db;
        }

        public async Task InsertAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO `ScheduleСlasses_Users` (`User_id`, `RecordingTime`, `isActive`) 
            VALUES (@User_id, @RecordingTime, @isActive);";
            BindParams(cmd);
            await cmd.ExecuteNonQueryAsync();
            ScheduleСlass_id = (int)cmd.LastInsertedId;
        }

        public async Task DeleteAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM ScheduleСlasses_Users WHERE ScheduleСlass_id = @ScheduleСlass_id;";
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task UpdateAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"UPDATE ScheduleСlasses_Users SET `isActive` = @isActive
            WHERE ScheduleСlass_id = @ScheduleСlass_id;";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        private void BindId(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@ScheduleСlass_id",
                DbType = DbType.Int32,
                Value = ScheduleСlass_id,
            });
        }

        private void BindParams(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@User_id",
                DbType = DbType.Int32,
                Value = User_id,
            });

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@RecordingTime",
                DbType = DbType.DateTime,
                Value = DateTime.Now,
            });

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@isActive",
                DbType = DbType.Boolean,
                Value = isActive,
            });
        }
    }
}

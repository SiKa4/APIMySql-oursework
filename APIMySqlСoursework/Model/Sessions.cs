using APIMySqlСoursework.DBMySql;
using Microsoft.AspNet.SignalR.Hubs;
using MySqlConnector;
using System.Data;
using System.Text.Json.Serialization;

namespace APIMySqlСoursework.Model
{
    public class Sessions
    {
        public string id_Session { get; set; }
        public int User_id { get; set; }
        public string UserIP { get; set; }
        public DateTime LastAuthorization { get; set; }
        internal DBConnection Db { get; set; }

        [JsonConstructor]
        public Sessions()
        {
        }

        internal Sessions(DBConnection db)
        {
            Db = db;
        }

        public async Task InsertAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO `Sessions` (`User_id`, `UserIP`, `LastAuthorization`) VALUES (@User_id, @UserIP, @LastAuthorization);";
            BindParams(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM `Sessions` WHERE `User_id` = @User_id;";
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task UpdateAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"UPDATE `Sessions` SET `UserIP` = @UserIP, `LastAuthorization` = @LastAuthorization WHERE `User_id` = @User_id;";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        private void BindId(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id_Session",
                DbType = DbType.String,
                Value = id_Session,
            });
        }

        private void BindParams(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@User_id",
                DbType = DbType.String,
                Value = User_id,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@UserIP",
                DbType = DbType.String,
                Value = UserIP,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@LastAuthorization",
                DbType = DbType.DateTime,
                Value = LastAuthorization,
            });
        }
    }
}

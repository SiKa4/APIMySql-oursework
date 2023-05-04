using APIMySqlСoursework.DBMySql;
using Microsoft.AspNet.SignalR.Hubs;
using MySqlConnector;
using System.Data;
using System.Text.Json.Serialization;

namespace APIMySqlСoursework.Model
{
    public class CoachRaiting
    {
        public int id_CoachRaiting { get; set; }
        public int Teacher_id { get; set; }
        public int User_id { get; set; }
        public double Raiting { get; set; }
        public string Review { get; set; }
        internal DBConnection Db { get; set; }

        [JsonConstructor]
        public CoachRaiting()
        {
        }

        internal CoachRaiting(DBConnection db)
        {
            Db = db;
        }

        public async Task InsertAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO `CoachRaiting` (`Teacher_id`, `User_id`, `Raiting`, `Review`) VALUES (@Teacher_id, @User_id, @Raiting, @Review);";
            BindParams(cmd);
            await cmd.ExecuteNonQueryAsync();
            id_CoachRaiting = (int)cmd.LastInsertedId;
        }

        public async Task DeleteAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM `CoachRaiting` WHERE `id_CoachRaiting` = @id_CoachRaiting;";
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task UpdateAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"UPDATE `CoachRaiting` SET `Teacher_id` = @Teacher_id, `User_id` = @User_id, `Raiting` = @Raiting, `Review` = @Review WHERE `id_CoachRaiting` = @id_CoachRaiting;";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        private void BindId(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id_CoachRaiting",
                DbType = DbType.Int32,
                Value = id_CoachRaiting,
            });
        }

        private void BindParams(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@Teacher_id",
                DbType = DbType.String,
                Value = Teacher_id,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@User_id",
                DbType = DbType.String,
                Value = User_id,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@Raiting",
                DbType = DbType.Double,
                Value = Raiting,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@Review",
                DbType = DbType.Int32,
                Value = Review,
            });
        }
    }
    public class CoachRaitingFullInfo
    {
        public int id_CoachRaiting { get; set; }
        public int Teacher_id { get; set; }
        public int User_id { get; set; }
        public double Raiting { get; set; }
        public string Review { get; set; }
        public string Teacher_Name { get; set; }
        public string User_Name { get; set; }

        internal DBConnection Db { get; set; }

        [JsonConstructor]
        public CoachRaitingFullInfo()
        {
        }

        internal CoachRaitingFullInfo(DBConnection db)
        {
            Db = db;
        }
    }
}

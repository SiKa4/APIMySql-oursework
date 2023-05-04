using APIMySqlСoursework.DBMySql;
using MySqlConnector;
using System.Data;
using System.Text.Json.Serialization;

namespace APIMySqlСoursework.Model
{
    public class Users
    {
        public int id_User { get; set; }
        public string FullName { get; set; }
        public int Role_id { get; set; }
        public string Number { get; set; }
        public string Role_Name { get; set; }
        internal DBConnection Db { get; set; }

        [JsonConstructor]
        public Users()
        {
        }

        internal Users(DBConnection db)
        {
            Db = db;
        }

        public async Task InsertAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO `Users` (`FullName`, `Role_id`, `Number`) VALUES (@FullName, @Role_id, @Number);";
            BindParams(cmd);
            await cmd.ExecuteNonQueryAsync();
            id_User = (int)cmd.LastInsertedId;
        }

        public async Task DeleteAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM `Users` WHERE `id_User` = @id_User;";
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task UpdateAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"UPDATE `Users` SET `FullName` = @FullName, `Role_id` = @Role_id, `Number` = @Number  WHERE `id_User` = @id_User;";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        private void BindId(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id_User",
                DbType = DbType.Int32,
                Value = id_User,
            });
        }

        private void BindParams(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@FullName",
                DbType = DbType.String,
                Value = FullName,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@Role_id",
                DbType = DbType.Int32,
                Value = Role_id,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@Number",
                DbType = DbType.String,
                Value = Number,
            });
        }


    }

    public class Coach
    {
        public int id_User { get; set; }
        public string FullName { get; set; }
        public int Role_id { get; set; }
        public string Number { get; set; }
        public string Description { get; set; }
        public string Image_URL { get; set; }
        public string Email { get; set; }

    }
}

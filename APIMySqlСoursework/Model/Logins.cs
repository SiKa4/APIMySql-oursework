using APIMySqlСoursework.DBMySql;
using MySqlConnector;
using System.Data;
using System.Text.Json.Serialization;

namespace APIMySqlСoursework.Model
{
    public class Logins
    {
        public int id_Login { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int User_id { get; set; }
        internal DBConnection Db { get; set; }

        [JsonConstructor]
        public Logins() { }
        internal Logins(DBConnection db)
        {
            Db = db;
        }

        public async Task InsertAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO `Logins` (`Login`, `Password`, `User_id`) VALUES (@Login, @Password, @User_id);";
            BindParams(cmd);
            await cmd.ExecuteNonQueryAsync();
            id_Login = (int)cmd.LastInsertedId;
        }

        public async Task DeleteAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM `Logins` WHERE `id_Login` = @id_Login;";
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task UpdateAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"UPDATE `Logins` SET `id_Login` = @id_Login, `Login` = @Login WHERE `Password` = @Password;";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        private void BindId(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id_Login",
                DbType = DbType.Int32,
                Value = id_Login,
            });
        }

        private void BindParams(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@Login",
                DbType = DbType.String,
                Value = Login,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@Password",
                DbType = DbType.String,
                Value = Password,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@User_id",
                DbType = DbType.Int32,
                Value = User_id,
            });
        }
    }
}
public class LogPass
{
    public string Login { get; set; }
    public string Password { get; set; }
    [JsonConstructor]
    public LogPass() { }
}

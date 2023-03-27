using MySqlConnector;

namespace APIMySqlСoursework.DBMySql
{
    public class DBConnection : IDisposable
    {
        public MySqlConnection Connection { get; } 

        public DBConnection(string connectionString)
        {
            Connection = new MySqlConnection(connectionString);
        }

        public void Dispose() => Connection.Dispose();
    }
}

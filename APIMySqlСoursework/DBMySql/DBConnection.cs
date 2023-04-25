using MySqlConnector;

namespace APIMySqlСoursework.DBMySql
{
    public class DBConnection : IDisposable
    {
        public MySqlConnection Connection { get; }
        public MySqlConnection Connection2 { get; }

        public DBConnection(string connectionString)
        {
            Connection = new MySqlConnection(connectionString);
            Connection2 = new MySqlConnection(connectionString);
        }

        public void Dispose()
        {
            Connection.Dispose();
            Connection2.Dispose();
        }
    }
}

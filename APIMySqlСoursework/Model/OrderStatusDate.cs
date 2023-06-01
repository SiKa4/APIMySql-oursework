using APIMySqlСoursework.DBMySql;
using Microsoft.AspNet.SignalR.Hubs;
using MySqlConnector;
using System.Data;
using System.Text.Json.Serialization;

namespace APIMySqlСoursework.Model
{
    public class OrderStatusDate
    {
        public int id_OrderStatusDate { get; set; }
        public int OrderStatus_id { get; set; }
        public DateTime DateOrder { get; set; }
        public int ShopOrder_id { get; set; }

        internal DBConnection Db { get; set; }

        [JsonConstructor]
        public OrderStatusDate()
        {
        }

        internal OrderStatusDate(DBConnection db)
        {
            Db = db;
        }

        public async Task InsertAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO `orderstatusdate` (`OrderStatus_id`, `DateOrder`, `ShopOrder_id`) VALUES (@OrderStatus_id, @DateOrder, @ShopOrder_id);";
            BindParams(cmd);
            await cmd.ExecuteNonQueryAsync();
            id_OrderStatusDate = (int)cmd.LastInsertedId;
        }

        public async Task DeleteAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM `orderstatusdate` WHERE `id_OrderStatusDate` = @id_OrderStatusDate;";
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task UpdateAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"UPDATE `orderstatusdate` SET `DateOrder` = @DateOrder, `ShopOrder_id` = @ShopOrder_id WHERE `id_OrderStatusDate` = @id_OrderStatusDate;";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        private void BindId(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id_OrderStatusDate",
                DbType = DbType.Int32,
                Value = id_OrderStatusDate,
            });
        }

        private void BindParams(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@OrderStatus_id",
                DbType = DbType.Int32,
                Value = OrderStatus_id,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@DateOrder",
                DbType = DbType.DateTime,
                Value = DateOrder,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@ShopOrder_id",
                DbType = DbType.Int32,
                Value = ShopOrder_id,
            });
        }
    }
    public class StatusAndDate
    {
        public int Status { get; set; }
        public DateTime DateOrder { get; set; }


        [JsonConstructor]
        public StatusAndDate() { }
    }
}

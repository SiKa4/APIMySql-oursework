using APIMySqlСoursework.DBMySql;
using APIMySqlСoursework.Model;
using System.Data.Common;

namespace APIMySqlСoursework.Query
{
    public class OrderStatusDateQuery
    {
        public DBConnection Db { get; }

        public OrderStatusDateQuery(DBConnection db)
        {
            Db = db;
        }

        public async Task<List<StatusAndDate>> FindAllAsync(int idShopOrder)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = $"SELECT os.Name, osd.DateOrder, osd.OrderStatus_id FROM orderstatusdate osd join orderstatus os on osd.OrderStatus_id = os.id_OrderStatus where osd.ShopOrder_id = {idShopOrder}";
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result : null;
        }

        private async Task<List<StatusAndDate>> ReadAllAsync(DbDataReader reader)
        {
            var statusAndDates = new List<StatusAndDate>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var data = new StatusAndDate()
                    {
                        Status = reader.GetString(0),
                        DateOrder = reader.GetDateTime(1),
                        OrderStatus_id = reader.GetInt32(2)
                    };
                    statusAndDates.Add(data);
                }
            }
            return statusAndDates;
        }
    }
}

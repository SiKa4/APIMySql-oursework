using APIMySqlСoursework.DBMySql;
using APIMySqlСoursework.Model;
using System.Data.Common;

namespace APIMySqlСoursework.Query
{
    public class ShopOrderQuery
    {
        public DBConnection Db { get; }

        public ShopOrderQuery(DBConnection db)
        {
            Db = db;
        }

        /*public async Task<ShopOrder> FindOneAsync(int id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = $"SELECT * FROM ShopOrders WHERE User_id = {id}";
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }*/

        public async Task<List<ShopOrder>> FindAllAsync(int id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = $"SELECT * FROM ShopOrders WHERE User_id = {id}";
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result : null;
        }
/*
        public async Task<List<ShopOrderFullInfo>> FindOneFullInfoAsync(int id_Order)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = $"SELECT so.id_Order, so.OrderStatus_id, so.User_id, os.Name, us.FullName FROM ShopOrders so JOIN Users us ON so.User_id = us.id_User JOIN OrderStatus os ON so.OrderStatus_id = os.id_OrderStatus WHERE so.id_Order = {id_Order};";
            var result = await ReadAllFullInfoAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }//ИЛЬЯС НАСРАЛ В ТРУСЫ ТУТ!!!!!!!*/


        private async Task<List<ShopOrder>> ReadAllAsync(DbDataReader reader)
        {
            var orders = new List<ShopOrder>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new ShopOrder(Db)
                    {
                        id_Order = reader.GetInt32(0),
                        OrderStatus_id = reader.GetInt32(1),
                    };
                    orders.Add(post);
                }
            }
            return orders;
        }

        private async Task<List<ShopOrderFullInfo>> ReadAllFullInfoAsync(DbDataReader reader)
        {
            var orders = new List<ShopOrderFullInfo>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new ShopOrderFullInfo(Db)
                    {
                        id_Order = reader.GetInt32(0),
                        OrderStatus_id = reader.GetInt32(1),
                        User_id = reader.GetInt32(2),
                        OrderStatus_Name = reader.GetString(3),
                        User_Name = reader.GetString(4),
                    };
                    orders.Add(post);
                }
            }
            return orders;
        }
    }
}

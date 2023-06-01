﻿using APIMySqlСoursework.DBMySql;
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
            cmd.CommandText = $"SELECT * FROM orderstatusdate osd join orderstatus os on osd.OrderStatus_id = os.id_OrderStatus where osd.ShopOrder_id = {idShopOrder}";
            await Db.Connection2.OpenAsync();
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            await Db.Connection2.CloseAsync();
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
                        Status = reader.GetInt32(0),
                        DateOrder = reader.GetDateTime(1)
                    };
                    statusAndDates.Add(data);
                }
            }
            return statusAndDates;
        }
    }
}

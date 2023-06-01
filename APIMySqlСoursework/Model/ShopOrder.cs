﻿using APIMySqlСoursework.DBMySql;
using MySqlConnector;
using System.Data;
using System.Text.Json.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace APIMySqlСoursework.Model
{
    public class ShopOrder
    {
        public int id_Order { get; set; }
        public int User_id { get; set; }
        public string PaymentId { get; set; }
        internal DBConnection Db { get; set; }

        [JsonConstructor]
        public ShopOrder()
        {
        }

        internal ShopOrder(DBConnection db)
        {
            Db = db;
        }

        public async Task InsertAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO ShopOrders (User_id) VALUES (@User_id)";
            BindParams(cmd);
            await cmd.ExecuteNonQueryAsync();
            id_Order = (int)cmd.LastInsertedId;
        }

        public async Task DeleteAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM `ShopOrders` WHERE `id_Order` = @id_Order;";
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task UpdateAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"UPDATE `ShopOrders` SET `PaymentId` = @PaymentId WHERE `id_Order` = @id_Order;";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        private void BindId(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id_Order",
                DbType = DbType.Int32,
                Value = id_Order,
            });
        }

        private void BindParams(MySqlCommand cmd)
        {
            
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@User_id",
                DbType = DbType.Int32,
                Value = User_id,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@PaymentId",
                DbType = DbType.String,
                Value = PaymentId,
            });
        }
    }

    public class ShopOrderFullInfo
    {
        public int id_Order { get; set; }
        public int OrderStatus_id { get; set; }
        public int User_id { get; set; }
        public string OrderStatus_Name { get; set; }
        public DateTime OrderDate { get; set; }
        public double TotalSum { get; set; }
        public List<ShopBasketFullInfo> ShopBaskets { get; set; }
        public List<StatusAndDate> StatusAndDates { get; set; }
        public string PaymentUri { get; set; }

        internal DBConnection Db { get; set; }
        [JsonConstructor]
        public ShopOrderFullInfo()
        {
        }

        internal ShopOrderFullInfo(DBConnection db)
        {
            Db = db;
        }
    }
}

using APIMySqlСoursework.DBMySql;
using Microsoft.AspNet.SignalR.Hubs;
using MySqlConnector;
using System.Data;
using System.Text.Json.Serialization;

namespace APIMySqlСoursework.Model
{
    //public class ShopBasketMini
    //{
    //    public int id_ShopBasket { get; set; }
    //    public int Order_id { get; set; }
    //}

    public class ShopBasket
    {
        public int id_ShopBasket { get; set; }
        public int ShopItem_id { get; set; }
        public int User_id { get; set; }
        public int ShopItemCount{ get; set; }
       
        internal DBConnection Db { get; set; }

        [JsonConstructor]
        public ShopBasket()
        {
        }

        internal ShopBasket(DBConnection db)
        {
            Db = db;
        }

        public async Task InsertAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO `ShopBasket` (`ShopItem_id`, `User_id`, `ShopItemCount`) VALUES (@ShopItem_id, @User_id, @ShopItemCount);";
            BindParams(cmd);
            await cmd.ExecuteNonQueryAsync();
            id_ShopBasket = (int)cmd.LastInsertedId;
        }

        public async Task DeleteAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM `ShopBasket` WHERE `id_ShopBasket` = @id_ShopBasket;";
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task UpdateAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"UPDATE ShopBasket SET ShopItem_id = @ShopItem_id, User_id = @User_id, ShopItemCount = @ShopItemCount WHERE id_ShopBasket = @id_ShopBasket;";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }


        private void BindId(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id_ShopBasket",
                DbType = DbType.Int32,
                Value = id_ShopBasket,
            });
        }

        private void BindParams(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@ShopItem_id",
                DbType = DbType.Int32,
                Value = ShopItem_id,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@User_id",
                DbType = DbType.Int32,
                Value = User_id,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@ShopItemCount",
                DbType = DbType.Int32,
                Value = ShopItemCount,
            });
        }
    }

    public class ShopBasketFullInfo
    {
        public int id_ShopBasket { get; set; }
        public int Item_id { get; set; }
        public int User_id { get; set; }
        public string Item_Name { get; set; }
        public string Item_Description { get; set; }
        public double Item_Price { get; set; }
        public int Item_Count { get; set; }
        public string User_Name { get; set; }
        public double FullPriceThisPosition { get; set; }
        public int ShopItemCount { get; set; }
        public string Image_URL { get; set; }
        public int Order_id { get; set; }
        internal DBConnection Db { get; set; }

        [JsonConstructor]
        public ShopBasketFullInfo()
        {
        }

        internal ShopBasketFullInfo(DBConnection db)
        {
            Db = db;
        }

        public async Task UpdateOrderIdAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = $"UPDATE `ShopBasket` SET `Order_id` = {Order_id} WHERE `id_ShopBasket` = {id_ShopBasket};";
            await cmd.ExecuteNonQueryAsync();
        }
    }
}

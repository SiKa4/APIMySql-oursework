using APIMySqlСoursework.DBMySql;
using MySqlConnector;
using System.Data;
using System.Text.Json.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace APIMySqlСoursework.Model
{
    public class ShopItems
    {

        public int id_ShopItem { get; set; }
        public string ShopItemName { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int ItemCount { get; set; }
        internal DBConnection Db { get; set; }

        [JsonConstructor]
        public ShopItems()
        {
        }

        internal ShopItems(DBConnection db)
        {
            Db = db;
        }

        public async Task InsertAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO `ShopItems` (`ShopItemName`, `Description`, `Price`, `ItemCount`) VALUES (@ShopItemName, @Description, @Price, @ItemCount);";
            BindParams(cmd);
            await cmd.ExecuteNonQueryAsync();
            id_ShopItem = (int)cmd.LastInsertedId;
        }

        public async Task DeleteAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM `ShopItems` WHERE `id_ShopItem` = @id_ShopItem;";
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task UpdateAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"UPDATE `ShopItems` SET `ShopItemName` = @ShopItemName, `Description` = @Description, `Price` = @Price, `ItemCount` = @ItemCount  WHERE `id_ShopItem` = @id_ShopItem;";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        private void BindId(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id_ShopItem",
                DbType = DbType.Int32,
                Value = id_ShopItem,
            });
        }

        private void BindParams(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@ShopItemName",
                DbType = DbType.String,
                Value = ShopItemName,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@Description",
                DbType = DbType.String,
                Value = Description,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@Price",
                DbType = DbType.Double,
                Value = Price,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@ItemCount",
                DbType = DbType.Int32,
                Value = ItemCount,
            });
        }
    }
}

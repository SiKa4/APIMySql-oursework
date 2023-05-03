using APIMySqlСoursework.DBMySql;
using APIMySqlСoursework.Model;
using System.Data.Common;

namespace APIMySqlСoursework.Query
{
    public class ShopItemQuery
    {
        public DBConnection Db { get; }

        public ShopItemQuery(DBConnection db)
        {
            Db = db;
        }

        public async Task<ShopItems> FindOneAsync(int id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = $"SELECT * FROM ShopItems WHERE id_ShopItem = {id}";
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }

        public async Task<List<ShopItems>> FindAllAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = $"SELECT * FROM ShopItems";
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result : null;
        }

        private async Task<List<ShopItems>> ReadAllAsync(DbDataReader reader)
        {
            var users = new List<ShopItems>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new ShopItems(Db)
                    {
                        id_ShopItem = reader.GetInt32(0),
                        ShopItemName = reader.GetString(1),
                        Description = reader.GetString(2),
                        Price = reader.GetDouble(3),
                        ItemCount = reader.GetInt32(4),
                        Image_URL = reader.GetString(5)
                    };
                    users.Add(post);
                }
            }
            return users;
        }
    }
}

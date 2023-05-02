using APIMySqlСoursework.DBMySql;
using APIMySqlСoursework.Model;
using System.Data.Common;

namespace APIMySqlСoursework.Query
{
    public class ShopBasketQuery
    {
        public DBConnection Db { get; }

        public ShopBasketQuery(DBConnection db)
        {
            Db = db;
        }
       
        public async Task<ShopBasket> FindOneAsync(int idShopBusket)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = $"SELECT * FROM ShopBasket WHERE id_ShopBasket = {idShopBusket} AND Order_id IS NULL";
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }

        public async Task<ShopBasket> FindOneAsync(int shopitem_id, int user_id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = $"SELECT * FROM ShopBasket WHERE ShopItem_id = {shopitem_id} AND User_id = {user_id} AND Order_id IS NULL";
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }

        public async Task<ShopBasket> FindOneByUserIdAsync(int idUser)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = $"SELECT * FROM ShopBasket WHERE User_id = {idUser} AND Order_id IS NULL";
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }

        public async Task<List<ShopBasket>> FindAllAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = $"SELECT * FROM ShopBasket WHERE AND Order_id IS NULL";
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result : null;
        }

        public async Task<List<ShopBasket>> FindAllAsync(int id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = $"SELECT * FROM ShopBasket WHERE User_id = {id} AND Order_id IS NULL";
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result : null;
        }

        public async Task<ShopBasketFullInfo> FindAllFullInfoByIdShopBusketAsync(int id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = $"Select sh.id_ShopBasket, si.id_ShopItem, us.id_User, si.ShopItemName, si.Description, si.Price, si.ItemCount, us.FullName From ShopBasket sh Join ShopItems si on sh.ShopItem_id = si.id_ShopItem Join Users us on sh.User_id = us.id_User WHERE id_ShopBasket = {id} AND sh.Order_id IS NULL;";
            var result = await ReadAllAsyncFullInfo(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }


        public async Task<ShopBasketFullInfo> FindAllFullInfoShopBusketAsync(int id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = $"Select sh.id_ShopBasket, si.id_ShopItem, us.id_User, si.ShopItemName, si.Description, si.Price, si.ItemCount, us.FullName From ShopBasket sh Join ShopItems si on sh.ShopItem_id = si.id_ShopItem Join Users us on sh.User_id = us.id_User AND sh.Order_id IS NULL;";
            var result = await ReadAllAsyncFullInfo(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }

        public async Task<List<ShopBasketFullInfo>> FindAllFullInfoByUserIdShopBusketAsync(int id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = $"Select sh.id_ShopBasket, si.id_ShopItem, us.id_User, si.ShopItemName, si.Description, si.Price, si.ItemCount, us.FullName, sh.ShopItemCount From ShopBasket sh Join ShopItems si on sh.ShopItem_id = si.id_ShopItem Join Users us on sh.User_id = us.id_User WHERE sh.User_id = {id} AND sh.Order_id IS NULL;";
            var result = await ReadAllAsyncFullInfo(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result : null;
        }


        private async Task<List<ShopBasket>> ReadAllAsync(DbDataReader reader)
        {
            var users = new List<ShopBasket>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new ShopBasket(Db)
                    {
                        id_ShopBasket = reader.GetInt32(0),
                        ShopItem_id = reader.GetInt32(1),
                        User_id = reader.GetInt32(2),
                        ShopItemCount = reader.GetInt32(3)
                    };
                    users.Add(post);
                }
            }
            return users;
        }

        private async Task<List<ShopBasketFullInfo>> ReadAllAsyncFullInfo(DbDataReader reader)
        {
            var shopBaskets = new List<ShopBasketFullInfo>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var shopBasket = new ShopBasketFullInfo(Db)
                    {
                        id_ShopBusket = reader.GetInt32(0),
                        Item_id = reader.GetInt32(1),
                        User_id = reader.GetInt32(2),
                        Item_Name = reader.GetString(3),
                        Item_Description = reader.GetString(4),
                        Item_Price = reader.GetDouble(5),
                        Item_Count = reader.GetInt32(6),
                        User_Name = reader.GetString(7),
                        ItemCount = reader.GetInt32(8),
                    };
                    shopBasket.FullPriceThisPosotion = (double)(shopBasket.Item_Count * shopBasket.Item_Price);
                    shopBaskets.Add(shopBasket);
                }
            }
            return shopBaskets;
        }
    }
}

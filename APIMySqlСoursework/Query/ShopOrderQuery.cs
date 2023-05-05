using APIMySqlСoursework.Controllers;
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

        public async Task<List<ShopOrderFullInfo>> FindAllFullInfoAsync(int idUser)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = $"SELECT sb.id_Order,sb.OrderStatus_id, sb.User_id, os.Name, sb.DateOrder FROM ShopOrders sb JOIN OrderStatus os ON sb.OrderStatus_id = os.id_OrderStatus WHERE User_id = {idUser}";
            await Db.Connection2.OpenAsync();
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            await Db.Connection2.CloseAsync();
            return result.Count > 0 ? result : null;
        }

        public async Task<ShopOrderFullInfo> FindAllIdsAsync(List<ClassInt> idsShopBusket, int idOrder)
        {
            using var cmd = Db.Connection.CreateCommand();
            var query = new ShopBasketQuery(Db);
            var shopItem = new ShopItemQuery(Db);
            foreach (var idShopBusket in idsShopBusket)
            {
                var temp = await query.FindOneAsync(idShopBusket.idShopBasket);
                if(temp is not null)
                {
                    temp.Order_id = idOrder;
                    var item = await shopItem.FindOneAsync(temp.ShopItem_id);
                    if (item.ItemCount - temp.ShopItemCount >= 0)
                    {
                        await shopItem.ChangeItemCountAsync(item.id_ShopItem, item.ItemCount - temp.ShopItemCount);
                        await temp.UpdateAsync();
                    }
                }
            }
            var answer = await FindAllFullInfoByOrderIdAsync(idOrder);
            answer.ShopBaskets = await query.FindAllFullInfoByOrderIdShopBusketAsync(idOrder);
            return answer;      
        }

        public async Task<ShopOrderFullInfo> FindAllFullInfoByOrderIdAsync(int idOrder)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = $"SELECT sb.id_Order,sb.OrderStatus_id, sb.User_id, os.Name, sb.DateOrder FROM ShopOrders sb JOIN OrderStatus os ON sb.OrderStatus_id = os.id_OrderStatus WHERE id_Order = {idOrder}";
            await Db.Connection2.OpenAsync();
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            await Db.Connection2.CloseAsync();
            return result.Count > 0 ? result[0] : null;
        }

        private async Task<List<ShopOrderFullInfo>> ReadAllAsync(DbDataReader reader)
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
                        OrderDate = reader.GetDateTime(4),
                    };
                    await CalculateOrderTotalSum(post);
                    orders.Add(post);
                }
                return orders;
            }
        }

        private async Task CalculateOrderTotalSum(ShopOrderFullInfo post)
        {
            using (var cmd = Db.Connection2.CreateCommand())
            {
                cmd.CommandText = $"Select si.Price, sb.ShopItemCount From ShopBasket sb JOIN ShopItems si ON sb.ShopItem_id = si.id_ShopItem where sb.Order_id = {post.id_Order}";
                var readerItemPrice = await cmd.ExecuteReaderAsync();
                while (readerItemPrice.Read()) post.TotalSum += (double)(readerItemPrice.GetDouble(0) * readerItemPrice.GetInt32(1));
                await readerItemPrice.DisposeAsync();
            }
        }
    }
}

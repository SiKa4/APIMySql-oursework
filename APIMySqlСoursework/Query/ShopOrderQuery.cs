using APIMySqlСoursework.Controllers;
using APIMySqlСoursework.DBMySql;
using APIMySqlСoursework.Model;
using APIMySqlСoursework.Payments;
using Microsoft.Extensions.Hosting;
using System.Data.Common;
using Yandex.Checkout.V3;

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
            cmd.CommandText = $"SELECT so.id_Order, so.User_id, so.PaymentId From shoporders so WHERE so.PaymentId IS NOT NULL AND so.User_id = {idUser}";
            await Db.Connection2.OpenAsync();
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync(), true);
            var basketQuery = new ShopBasketQuery(Db);
            var statusDateQuery = new OrderStatusDateQuery(Db);
            foreach (var item in result)
            {
                item.ShopBaskets = await basketQuery.FindAllFullInfoByOrderIdShopBusketAsync(item.id_Order);
                item.StatusAndDates = await statusDateQuery.FindAllAsync(item.id_Order);
            }
            await Db.Connection2.CloseAsync();
            return result.Count > 0 ? result : null;
        }

        public async Task<ShopOrder> FindOrderByPaymentIdAsync(string idPayment)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = $"SELECT * FROM ShopOrders WHERE `PaymentId` = '{idPayment}'";
            var result = await ReadAsyncMinInfo(await cmd.ExecuteReaderAsync());
            return result;
        }

        public async Task<ShopOrderFullInfo> FindAllFullInfoByOrderIdAsync(int idOrder)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = $"SELECT so.id_Order, so.User_id From ShopOrders so Join OrderStatusDate osd on so.id_Order = osd.ShopOrder_id join OrderStatus os on osd.OrderStatus_id = os.id_OrderStatus where so.id_Order = {idOrder}";
            await Db.Connection2.OpenAsync();
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync(), false);
            await Db.Connection2.CloseAsync();
            return result.Count > 0 ? result[0] : null;
        }

        public async Task<ShopOrderFullInfo> FindAllIdsAsync(List<ClassInt> idsShopBusket, int idOrder)
        {
            using var cmd = Db.Connection.CreateCommand();
            var query = new ShopBasketQuery(Db);
            var shopItem = new ShopItemQuery(Db);
            foreach (var idShopBusket in idsShopBusket)
            {
                var temp = await query.FindOneAsync(idShopBusket.idShopBasket);
                if (temp is not null)
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
            if(answer != null)
                answer.ShopBaskets = await query.FindAllFullInfoByOrderIdShopBusketAsync(idOrder);
            return answer;
        } 

        private async Task<ShopOrder> ReadAsyncMinInfo(DbDataReader reader)
        {
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var orders = new ShopOrder(Db)
                    {
                        id_Order = reader.GetInt32(0),
                        User_id = reader.GetInt32(1),
                        PaymentId = reader.GetString(2),
                    };
                    return orders;
                }
                return null;
            }
        }

        private async Task<List<ShopOrderFullInfo>> ReadAllAsync(DbDataReader reader, bool isFiveElement)
        {
            var orders = new List<ShopOrderFullInfo>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new ShopOrderFullInfo(Db)
                    {
                        id_Order = reader.GetInt32(0),
                        User_id = reader.GetInt32(1),
                    };
                    if (isFiveElement) post.PaymentUri = $"https://yoomoney.ru/checkout/payments/v2/contract?orderId={reader.GetString(2)}";
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

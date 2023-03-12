using Microsoft.EntityFrameworkCore;
using OrdersApiAppPV012.Data;
using OrdersApiAppPV012.Models.Entities;
using OrdersApiAppPV012.Services.Interfaces;

namespace OrdersApiAppPV012.Services.Repositories
{
    public class DaoOrderInfo : IDaoOrderInfo
    {
        private readonly AppDbContext db;

        public DaoOrderInfo(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<List<string>> GetOrderInfo(Guid orderId)
        {
            var order = db.Orders.FirstOrDefault(o => o.Id == orderId);

            var orderProducts = db.OrderProducts
                .Where(o => o.OrderId == orderId)
                .Include(p => p.Product);
                //.Include(o => o.Order);

            // Инфа о заказе
            var orderInfo = new List<string>();
            // Количество всех товаров в заказе
            int countProducns = 0;

            orderInfo.Add($"Описание заказа: {order.Description}");

            foreach (var product in orderProducts)
            {
                orderInfo.Add(
                    $"Товар: {product.Product.Title} | " +
                    $"Количество: {product.CountProducts} шт"
                );
                countProducns += product.CountProducts;
            }

            orderInfo.Add($"Всего товаров: {countProducns} шт");

            return orderInfo;
        }
    }
}

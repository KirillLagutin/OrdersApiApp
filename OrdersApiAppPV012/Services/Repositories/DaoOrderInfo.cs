using Microsoft.EntityFrameworkCore;
using OrdersApiAppPV012.Data;
using OrdersApiAppPV012.Services.Interfaces;

namespace OrdersApiAppPV012.Services.Repositories
{
    // Реализация инфы о заказе
    public class DaoOrderInfo : IDaoOrderInfo
    {
        private readonly AppDbContext db;

        public DaoOrderInfo(AppDbContext db)
        {
            this.db = db;
        }

        // Метод вывода инфы о заказе
        public async Task<IResult> GetOrderInfo(Guid orderId)
        {
            var order = await db.Orders.FirstOrDefaultAsync(o => o.Id == orderId);

            if (order is null)
            {
                return Results.NotFound(new {message = $"Нет заказа с ID = {orderId}"}); 
            }

            // Получаем расшивки с нашим заказом (и с данными о продукте)
            var orderProducts = db.OrderProducts
                .Where(op => op.OrderId == orderId)
                .Include(p => p.Product);

            // Инфа о заказе
            var orderInfo = new List<string>();

            // Количество всех товаров в заказе
            int countProducns = 0;

            orderInfo.Add($"Описание заказа: {order.Description}");

            foreach (var product in orderProducts)
            {
                orderInfo.Add(
                    $"Товар: {product.Product?.Title} | " +
                    $"Количество: {product.CountProducts} шт"
                );
                countProducns += product.CountProducts;
            }

            orderInfo.Add($"Всего товаров: {countProducns} шт");

            return Results.Json(orderInfo);
        }
    }
}

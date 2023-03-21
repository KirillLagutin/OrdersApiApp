using Microsoft.EntityFrameworkCore;
using OrdersApiAppPV012.Data;
using OrdersApiAppPV012.Models;
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
                return Results.NotFound(new { message = $"Нет заказа с ID = {orderId}" });
            }

            // Получаем расшивки с нашим заказом (и с данными о продукте)
            var orderProducts = db.OrderProducts
                .Where(op => op.OrderId == orderId)
                .Include(p => p.Product);

            // Объект для ответа на основе класса OrderInfo
            var orderInfo = new OrderInfo()
            {
                Id = orderId,
                OrderDescription = order.Description,
                ProductTitleAndCount = new(),
                CountAllProducts = 0
            };

            // Заполняем связку товар-количество из расшивки
            foreach (var product in orderProducts)
            {
                orderInfo.ProductTitleAndCount.Add(product.Product.Title, product.CountProducts);
                orderInfo.CountAllProducts += product.CountProducts;
            }

            return Results.Ok(orderInfo);
        }
    }
}
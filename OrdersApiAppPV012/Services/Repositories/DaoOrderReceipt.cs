using Microsoft.EntityFrameworkCore;
using OrdersApiAppPV012.Data;
using OrdersApiAppPV012.Services.Interfaces;

namespace OrdersApiAppPV012.Services.Repositories
{
    // Реализация суммы заказа
    public class DaoOrderReceipt : IDaoOrderReceipt
    {
        private readonly AppDbContext db;

        public DaoOrderReceipt(AppDbContext db)
        {
            this.db = db;
        }

        // Метод вывода суммы заказа
        public async Task<IResult> GetOrderReceipt(Guid orderId)
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

            // Чек заказа
            var check = new List<string>();

            // Общая сумма заказа
            decimal totalSum = 0;

            check.Add($"Описание заказа: {order.Description}");

            foreach (var product in orderProducts)
            {
                check.Add(
                    $"Товар: {product.Product?.Title} | " +
                    $"{product.CountProducts} шт. | " +
                    $"Цена: {product.Product?.Price} р."
                );
                totalSum += product.CountProducts * product.Product.Price;
            }

            check.Add($"Общая сумма заказа: {totalSum} р.");

            return Results.Json(check);
        }
    }
}

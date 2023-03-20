using Microsoft.EntityFrameworkCore;
using OrdersApiAppPV012.Data;
using OrdersApiAppPV012.Models;
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
            var check = new OrderReceipt()
            {
                Id = orderId,
                ProductTitle = new(),
                ProductCount = new(),
                Price = new(),
                TotalSum = 0
            };
            // Добавляем в чек инфу с расшивки
            foreach (var product in orderProducts)
            {
                check.ProductTitle.Add(product.Product.Title);
                check.ProductCount.Add(product.CountProducts);
                check.Price.Add(product.Product.Price);

                check.TotalSum += product.CountProducts * product.Product.Price;
            }

            return Results.Ok(check);
        }
    }
}

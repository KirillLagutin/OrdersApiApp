using Microsoft.EntityFrameworkCore;
using OrdersApiAppPV012.Data;
using OrdersApiAppPV012.Models.Entities;
using OrdersApiAppPV012.Services.Interfaces;

namespace OrdersApiAppPV012.Services.Repositories.CRUD
{
    public class DbDaoOrderProduct : IDaoBase<OrderProduct>
    {
        // Имплементация dao, работающая с БД
        private readonly AppDbContext db;

        public DbDaoOrderProduct(AppDbContext db)
        {
            this.db = db;
        }


        // Вернуть все заказ-товары
        public async Task<IResult> GetAllItems()
        {
            var orderProducts = await db.OrderProducts.ToListAsync();

            if (orderProducts.Count == 0)
            {
                return Results.NotFound(new { message = "Список пуст" });
            }

            return Results.Ok(orderProducts);
        }


        // Вернуть заказ-товар по Id
        public async Task<IResult> GetItemById(Guid id)
        {
            if (id.GetType() != typeof(Guid))
            {
                return Results.BadRequest(new { message = "Неверный ID" });
            }

            var orderProduct = await db.OrderProducts.FirstOrDefaultAsync(c => c.Id == id);

            if (orderProduct is null)
            {
                return Results.NotFound(new { message = "Нет элемента по такому ID" });
            }

            return Results.Ok(orderProduct);
        }

        // Добавить заказ-товар
        public async Task<IResult> AddItem(OrderProduct orderProduct)
        {
            await db.OrderProducts.AddAsync(orderProduct);
            await db.SaveChangesAsync();

            return Results.Ok(orderProduct);
        }

        // Обновить заказ-товар
        public async Task<IResult> UpdateItem(OrderProduct orderProduct)
        {
            db.OrderProducts.Entry(orderProduct).State = EntityState.Modified;
            await db.SaveChangesAsync();

            return Results.Ok(orderProduct);
        }

        // Удалить заказ-товар
        public async Task<IResult> DeleteItem(Guid id)
        {
            var orderProduct = await db.OrderProducts.FirstOrDefaultAsync(c => c.Id == id);

            if (orderProduct is null)
            {
                return Results.NotFound(new { message = "Нет элемента по такому ID" });
            }

            db.OrderProducts.Remove(orderProduct);
            await db.SaveChangesAsync();

            return Results.Ok(new { message = "Элемент удалён" });
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrdersApiAppPV012.Data;
using OrdersApiAppPV012.Models.Entities;
using OrdersApiAppPV012.Services.Interfaces;

namespace OrdersApiAppPV012.Services.Repositories.CRUD
{
    public class DbDaoOrder : IDaoBase<Order>
    {
        // Имплементация dao, работающая с БД
        private readonly AppDbContext db;

        public DbDaoOrder(AppDbContext db)
        {
            this.db = db;
        }


        // Вернуть список заказов
        public async Task<IResult> GetAllItems()
        {
            var orders = await db.Orders.ToListAsync();

            if (orders.Count == 0)
            {
                return Results.NotFound(new { message = "Список пуст" });
            }

            return Results.Ok(orders);
        }

        // Вернуть заказ по Id
        public async Task<IResult> GetItemById(Guid id)
        {
            if (id.GetType() != typeof(Guid))
            {
                return Results.BadRequest(new { message = "Неверный ID" });
            }

            var order = await db.Orders.FirstOrDefaultAsync(c => c.Id == id);

            if (order is null)
            {
                return Results.NotFound(new { message = "Нет элемента по такому ID" });
            }

            return Results.Ok(order);
        }
            
        // Добавить заказ
        public async Task<IResult> AddItem(Order order)
        {
            await db.Orders.AddAsync(order);
            await db.SaveChangesAsync();

            return Results.Ok(order);
        }

        // Обновить заказ
        public async Task<IResult> UpdateItem(Order order)
        {
            db.Orders.Entry(order).State = EntityState.Modified;
            await db.SaveChangesAsync();

            return Results.Ok(order);
        }

        // Удалить заказ
        public async Task<IResult> DeleteItem(Guid id)
        {
            var order = await db.Orders.FirstOrDefaultAsync(c => c.Id == id);

            if (order is null)
            {
                return Results.NotFound(new { message = "Нет элемента по такому ID" });
            }

            db.Orders.Remove(order);
            await db.SaveChangesAsync();

            return Results.Ok(new { message = "Элемент удалён" });
        }
    }
}

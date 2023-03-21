using Microsoft.EntityFrameworkCore;
using OrdersApiAppPV012.Data;
using OrdersApiAppPV012.Models.Entities;
using OrdersApiAppPV012.Services.Interfaces;

namespace OrdersApiAppPV012.Services.Repositories.CRUD
{
    // Имплементация dao, работающая с БД
    public class DbDaoClient : IDaoBase<Client>
    {
        private readonly AppDbContext db;

        public DbDaoClient(AppDbContext db)
        {
            this.db = db;
        }


        // Вернуть список клиентов
        public async Task<IResult> GetAllItems()
        {
            var clients = await db.Clients.ToListAsync();

            if (clients.Count == 0)
            {
                return Results.NotFound(new { message = "Список пуст" });
            }

            return Results.Ok(clients);
        }

        // Вернуть клиента по Id
        public async Task<IResult> GetItemById(Guid id)
        {
            if (id.GetType() != typeof(Guid))
            {
                return Results.BadRequest(new { message = "Неверный ID" });
            }

            var client = await db.Clients.FirstOrDefaultAsync(c => c.Id == id);

            if (client is null)
            {
                return Results.NotFound(new { message = "Нет элемента по такому ID" });
            }

            return Results.Ok(client);
        }

        // Добавить клиента
        public async Task<IResult> AddItem(Client client)
        {
            await db.Clients.AddAsync(client);
            await db.SaveChangesAsync();

            return Results.Ok(client);
        }

        // Обновить клиента
        public async Task<IResult> UpdateItem(Client client)
        {
            db.Clients.Entry(client).State = EntityState.Modified;
            await db.SaveChangesAsync();

            return Results.Ok(client);
        }

        // Удалить клиента
        public async Task<IResult> DeleteItem(Guid id)
        {
            var client = await db.Clients.FirstOrDefaultAsync(c => c.Id == id);

            if (client is null)
            {
                return Results.NotFound(new { message = "Нет элемента по такому ID" });
            }

            db.Clients.Remove(client);
            await db.SaveChangesAsync();

            return Results.Ok(new { message = "Элемент удалён" });
        }
    }
}

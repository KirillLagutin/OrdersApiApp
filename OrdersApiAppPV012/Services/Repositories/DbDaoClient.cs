using Microsoft.EntityFrameworkCore;
using OrdersApiAppPV012.Data;
using OrdersApiAppPV012.Models.Entities;
using OrdersApiAppPV012.Services.Interfaces;

namespace OrdersApiAppPV012.Services.Repositories
{
    // имплементация dao, работающая с БД
    public class DbDaoClient : IDaoBase<Client>
    {
        private readonly AppDbContext db;

        public DbDaoClient(AppDbContext db)
        {
            this.db = db;
        }


        // Вернуть всех клиентов
        public async Task<List<Client>> GetAllItems() =>
            await db.Clients.ToListAsync();

        // Вернуть клиента по Id
        public async Task<Client?> GetItemById(Guid id) =>
            await db.Clients.FirstOrDefaultAsync(c => c.Id == id);
            
        // Добавить клиента
        public async Task<Client> AddItem(Client client)
        {
            await db.Clients.AddAsync(client);
            await db.SaveChangesAsync();
            return client;
        }

        // Обновить клиента
        public async Task<Client> UpdateItem(Client client)
        {
            db.Clients.Entry(client).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return client;
        }

        // Удалить клиента
        public async Task<bool> DeleteItem(Guid id)
        {
            var client = await db.Clients.FirstOrDefaultAsync(c => c.Id == id);
            db.Clients.Remove(client);
            await db.SaveChangesAsync();
            return true;
        }
    }
}

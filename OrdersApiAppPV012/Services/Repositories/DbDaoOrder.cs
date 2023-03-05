using Microsoft.EntityFrameworkCore;
using OrdersApiAppPV012.Data;
using OrdersApiAppPV012.Models.Entities;
using OrdersApiAppPV012.Services.Interfaces;

namespace OrdersApiAppPV012.Services.Repositories
{
    public class DbDaoOrder : IDaoBase<Order>
    {
        // имплементация dao, работающая с БД
        private readonly AppDbContext db;

        public DbDaoOrder(AppDbContext db)
        {
            this.db = db;
        }


        // Вернуть все заказы
        public async Task<List<Order>> GetAllItems()
        {
            db.Clients.Load(); // явная выгрузка данных
                                
            return await db.Orders.ToListAsync();
        }
            
        // Вернуть заказ по Id
        public async Task<Order?> GetItemById(Guid id) =>
            await db.Orders.FirstOrDefaultAsync(c => c.Id == id);

        // Добавить заказ
        public async Task<Order> AddItem(Order orders)
        {
            await db.Orders.AddAsync(orders);
            await db.SaveChangesAsync();
            return orders;
        }

        // Обновить заказ
        public async Task<Order> UpdateItem(Order orders)
        {
            db.Orders.Entry(orders).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return orders;
        }

        // Удалить заказ
        public async Task<bool> DeleteItem(Guid id)
        {
            var orders = await db.Orders.FirstOrDefaultAsync(c => c.Id == id);
            db.Orders.Remove(orders);
            await db.SaveChangesAsync();
            return true;
        }
        
    }
}

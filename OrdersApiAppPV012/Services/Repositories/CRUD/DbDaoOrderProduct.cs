using Microsoft.EntityFrameworkCore;
using OrdersApiAppPV012.Data;
using OrdersApiAppPV012.Models.Entities;
using OrdersApiAppPV012.Services.Interfaces;

namespace OrdersApiAppPV012.Services.Repositories.CRUD
{
    public class DbDaoOrderProduct : IDaoBase<OrderProduct>
    {
        // имплементация dao, работающая с БД
        private readonly AppDbContext db;

        public DbDaoOrderProduct(AppDbContext db)
        {
            this.db = db;
        }


        // Вернуть все заказ-товары
        public async Task<IReadOnlyList<OrderProduct>> GetAllItems() =>
            await db.OrderProducts.ToListAsync();


        // Вернуть заказ-товар по Id
        public async Task<OrderProduct?> GetItemById(Guid id) =>
            await db.OrderProducts.FirstOrDefaultAsync(c => c.Id == id);

        // Добавить заказ-товар
        public async Task<OrderProduct> AddItem(OrderProduct orderProduct)
        {
            await db.OrderProducts.AddAsync(orderProduct);
            await db.SaveChangesAsync();
            return orderProduct;
        }

        // Обновить заказ-товар
        public async Task<OrderProduct> UpdateItem(OrderProduct orderProduct)
        {
            db.OrderProducts.Entry(orderProduct).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return orderProduct;
        }

        // Удалить заказ-товар
        public async Task<bool> DeleteItem(Guid id)
        {
            var orderProduct = await db.OrderProducts.FirstOrDefaultAsync(c => c.Id == id);
            db.OrderProducts.Remove(orderProduct);
            await db.SaveChangesAsync();
            return true;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using OrdersApiAppPV012.Data;
using OrdersApiAppPV012.Models.Entities;
using OrdersApiAppPV012.Services.Interfaces;

namespace OrdersApiAppPV012.Services.Repositories
{
    // имплементация dao, работающая с БД
    public class DbDaoProduct : IDaoBase<Product>
    {
        private readonly AppDbContext db;

        public DbDaoProduct(AppDbContext db)
        {
            this.db = db;
        }


        // Вернуть всех клиентов
        public async Task<List<Product>> GetAllItems() =>
            await db.Products.ToListAsync();

        // Вернуть клиента по Id
        public async Task<Product?> GetItemById(Guid id) =>
            await db.Products.FirstOrDefaultAsync(c => c.Id == id);

        // Добавить клиента
        public async Task<Product> AddItem(Product product)
        {
            await db.Products.AddAsync(product);
            await db.SaveChangesAsync();
            return product;
        }

        // Обновить клиента
        public async Task<Product> UpdateItem(Product product)
        {
            db.Products.Entry(product).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return product;
        }

        // Удалить клиента
        public async Task<bool> DeleteItem(Guid id)
        {
            var product = await db.Products.FirstOrDefaultAsync(c => c.Id == id);
            db.Products.Remove(product);
            await db.SaveChangesAsync();
            return true;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using OrdersApiAppPV012.Data;
using OrdersApiAppPV012.Models.Entities;
using OrdersApiAppPV012.Services.Interfaces;

namespace OrdersApiAppPV012.Services.Repositories.CRUD
{
    // Имплементация dao, работающая с БД
    public class DbDaoProduct : IDaoBase<Product>
    {
        private readonly AppDbContext db;

        public DbDaoProduct(AppDbContext db)
        {
            this.db = db;
        }


        // Вернуть список продуктов
        public async Task<IResult> GetAllItems()
        {
            var products = await db.Products.ToListAsync();

            if (products.Count == 0)
            {
                return Results.NotFound(new { message = "Список пуст" });
            }

            return Results.Json(products);
        }

        // Вернуть продукт по Id
        public async Task<IResult> GetItemById(Guid id)
        {
            if (id.GetType() != typeof(Guid))
            {
                return Results.BadRequest(new { message = "Неверный ID" });
            }

            var product = await db.Products.FirstOrDefaultAsync(c => c.Id == id);

            if (product is null)
            {
                return Results.NotFound(new { message = "Нет элемента по такому ID" });
            }

            return Results.Json(product);
        }

        // Добавить продукт
        public async Task<IResult> AddItem(Product product)
        {
            await db.Products.AddAsync(product);
            await db.SaveChangesAsync();

            return Results.Json(product);
        }

        // Обновить продукт
        public async Task<IResult> UpdateItem(Product product)
        {
            db.Products.Entry(product).State = EntityState.Modified;
            await db.SaveChangesAsync();

            return Results.Json(product);
        }

        // Удалить продукт
        public async Task<IResult> DeleteItem(Guid id)
        {
            var product = await db.Products.FirstOrDefaultAsync(c => c.Id == id);

            if (product is null)
            {
                return Results.NotFound(new { message = "Нет элемента по такому ID" });
            }

            db.Products.Remove(product);
            await db.SaveChangesAsync();

            return Results.Ok(new { message = "Элемент удалён" });
        }
    }
}

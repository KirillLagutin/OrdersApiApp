using OrdersApiAppPV012.Data;
using OrdersApiAppPV012.Models.Entities;
using OrdersApiAppPV012.Services.Interfaces;
using OrdersApiAppPV012.Services.Repositories;

var builder = WebApplication.CreateBuilder(args);

// добавление зависимостей
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddTransient<IDaoBase<Client>, DbDaoClient>();
builder.Services.AddTransient<IDaoBase<Product>, DbDaoProduct>();

var app = builder.Build();


app.MapGet("/", () => "МОЙ МАГАЗИН");


// CRUD для Клиента

app.MapGet("/client/all", async (HttpContext context, IDaoBase<Client> dao) =>
{
    return await dao.GetAllItems();
});

app.MapPost("/client/add", async (HttpContext context, IDaoBase<Client> dao, Client client) =>
{
    return await dao.AddItem(client);
});

app.MapGet("/client/get", async (HttpContext context, IDaoBase<Client> dao, Guid id) =>
{
    return await dao.GetItemById(id);
});

app.MapPost("/client/update", async (HttpContext context, IDaoBase<Client> dao, Client client) =>
{
    return await dao.UpdateItem(client);
});

app.MapPost("/client/delete", async (HttpContext context, IDaoBase<Client> dao, Guid id) =>
{
    return await dao.DeleteItem(id);
});


// CRUD для Продукта

app.MapGet("/product/all", async (HttpContext context, IDaoBase<Product> dao) =>
{
    return await dao.GetAllItems();
});

app.MapPost("/product/add", async (HttpContext context, IDaoBase<Product> dao, Product product) =>
{
    return await dao.AddItem(product);
});

app.MapGet("/product/get", async (HttpContext context, IDaoBase<Product> dao, Guid id) =>
{
    return await dao.GetItemById(id);
});

app.MapPost("/product/update", async (HttpContext context, IDaoBase<Product> dao, Product product) =>
{
    return await dao.UpdateItem(product);
});

app.MapPost("/product/delete", async (HttpContext context, IDaoBase<Product> dao, Guid id) =>
{
    return await dao.DeleteItem(id);
});


app.Run();

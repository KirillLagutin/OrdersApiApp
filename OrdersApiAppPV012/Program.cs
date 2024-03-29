using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OrdersApiAppPV012.Data;
using OrdersApiAppPV012.Models.Entities;
using OrdersApiAppPV012.Services;
using OrdersApiAppPV012.Services.Interfaces;
using OrdersApiAppPV012.Services.Repositories;
using OrdersApiAppPV012.Services.Repositories.CRUD;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// ���������� ����������� DbContext //
builder.Services.AddDbContext<AppDbContext>();

// ����������� �������� //

// CRUD
builder.Services.AddTransient<IDaoBase<Client>, DbDaoClient>();
builder.Services.AddTransient<IDaoBase<Product>, DbDaoProduct>();
builder.Services.AddTransient<IDaoBase<Order>, DbDaoOrder>();
builder.Services.AddTransient<IDaoBase<OrderProduct>, DbDaoOrderProduct>();
// �������������� ������
builder.Services.AddTransient<IDaoOrderInfo, DaoOrderInfo>(); // ���� � ������
builder.Services.AddTransient<IDaoOrderReceipt, DaoOrderReceipt>(); // ��� ������

// JWT
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // ���������, ����� �� �������������� �������� ��� ��������� ������
            ValidateIssuer = true,
            // ������, �������������� ��������
            ValidIssuer = AuthOptions.ISSUER,
            // ����� �� �������������� ����������� ������
            ValidateAudience = true,
            // ��������� ����������� ������
            ValidAudience = AuthOptions.AUDIENCE,
            // ����� �� �������������� ����� �������������
            ValidateLifetime = true,
            // ��������� ����� ������������
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            // ��������� ����� ������������
            ValidateIssuerSigningKey = true,
        };
    });

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.Map("/login/{username}", (string username) =>
{
    var claims = new List<Claim> { new Claim(ClaimTypes.Name, username) };
    // ������� JWT-�����
    var jwt = new JwtSecurityToken(
            issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

    return new JwtSecurityTokenHandler().WriteToken(jwt);
});


app.MapGet("/",  () => "Order Api");

// CRUD ��������� //

// �����
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

app.MapPost("/client/update",  async (HttpContext context, IDaoBase<Client> dao, Client client) =>
{
    return await dao.UpdateItem(client);
});

app.MapPost("/client/delete", async (HttpContext context, IDaoBase<Client> dao, Guid id) =>
{
    return await dao.DeleteItem(id);
});


// �������
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


// �����
app.MapGet("/order/all", async (HttpContext context,  IDaoBase<Order> dao) =>
{
    return await dao.GetAllItems();
});

app.MapPost("/order/add", async (HttpContext context, IDaoBase<Order> dao, Order order) =>
{
    return await dao.AddItem(order);
});

app.MapGet("/order/get", async (HttpContext context, IDaoBase<Order> dao, Guid id) =>
{
    return await dao.GetItemById(id);
});

app.MapPost("/order/update", async (HttpContext context, IDaoBase<Order> dao, Order order) =>
{
    return await dao.UpdateItem(order);
});

app.MapPost("/order/delete", async (HttpContext context, IDaoBase<Order> dao, Guid id) =>
{
    return await dao.DeleteItem(id);
});


// �����-�����
app.MapGet("/order_product/all", async (HttpContext context, IDaoBase<OrderProduct> dao) =>
{
    return await dao.GetAllItems();
});

app.MapPost("/order_product/add", async (HttpContext context, IDaoBase<OrderProduct> dao, OrderProduct orderProduct) =>
{
    return await dao.AddItem(orderProduct);
});

app.MapGet("/order_product/get", async (HttpContext context, IDaoBase<OrderProduct> dao, Guid id) =>
{
    return await dao.GetItemById(id);
});

app.MapPost("/order_product/update", async (HttpContext context, IDaoBase<OrderProduct> dao, OrderProduct orderProduct) =>
{
    return await dao.UpdateItem(orderProduct);
});

app.MapPost("/order_product/delete", async (HttpContext context, IDaoBase<OrderProduct> dao, Guid id) =>
{
    return await dao.DeleteItem(id);
});


// �������������� ������ //

// ���� � ������
app.MapGet("/order/info", [Authorize] async (HttpContext context, IDaoOrderInfo dao, Guid id) =>
{
    return await dao.GetOrderInfo(id);
});

// ��� ������
app.MapGet("/order/receipt", [Authorize] async (HttpContext context, IDaoOrderReceipt dao, Guid id) =>
{
    return await dao.GetOrderReceipt(id);
});


app.Run();

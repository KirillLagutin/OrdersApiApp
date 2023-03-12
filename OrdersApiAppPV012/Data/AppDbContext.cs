using Microsoft.EntityFrameworkCore;
using OrdersApiAppPV012.Models.Entities;

namespace OrdersApiAppPV012.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Client> Clients => Set<Client>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderProduct> OrderProducts => Set<OrderProduct>();

        // конфигурация контекста
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // получаем файл конфигурации
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            // устанавливаем для контекста строку подключения
            // инициализируем саму строку подключения
            string? useConnection = configuration.GetSection("UseConnectionString").Value;

            // Для подключения базы в контейнере
            //optionsBuilder.UseNpgsql(configuration.GetConnectionString(useConnection));

            // Для подключения облачной базы
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        }
    }
}

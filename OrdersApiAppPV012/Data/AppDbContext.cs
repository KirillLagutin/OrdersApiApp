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

        // Конфигурация контекста
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Получаем файл конфигурации
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            // Облачная база, закомментировать 26-ю и 29-ю строку
            string useConnection = "DefaultConnection";

            // База для работы в контейнере, закомментировать 29-ю строку
             useConnection = "DockerAppLocalhost";

            // База для билда контейнера
            useConnection = "DockerAppDb";

            optionsBuilder.UseNpgsql(configuration.GetConnectionString(useConnection) ?? 
                throw new InvalidOperationException("Connection string 'AppDbContext' not found."));
        }
    }
}

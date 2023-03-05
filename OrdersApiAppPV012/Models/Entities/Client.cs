using System.Text.Json.Serialization;

namespace OrdersApiAppPV012.Models.Entities
{
    public class Client
    {
        public Guid Id { get; init; }

        public string Name { get; set; } = "Имя клиента";

        [JsonIgnore]
        public ICollection<Order>? Orders { get; set; }
    }
}

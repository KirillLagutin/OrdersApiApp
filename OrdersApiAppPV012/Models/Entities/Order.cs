using System.ComponentModel.DataAnnotations.Schema;

namespace OrdersApiAppPV012.Models.Entities
{
    public class Order
    {
        public Guid Id { get; init; }

        public string Description { get; set; } = "Описание заказа";

        public Guid ClientId { get; set; }

        public Client? Client { get; set; }

        public ICollection<OrderProduct>? OrderProducts { get; set; }
    }
}

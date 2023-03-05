namespace OrdersApiAppPV012.Models.Entities
{
    public class OrderProduct
    {
        public Guid Id { get; set; }

        public Guid OrderId { get; init; }

        public Guid ProductId { get; set; }

        public int CountProducts { get; set; } = default;

        public Order? Order { get; set; }

        public Product? Product { get; set; }
    }
}

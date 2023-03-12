namespace OrdersApiAppPV012.Models.Entities
{
    public class Product
    {
        public Guid Id { get; init; }

        public string Title { get; set; } = "Название продукта";

        public decimal Price { get; set; }

        public int Article { get; set; }

        public ICollection<OrderProduct>? OrderProducts { get; set; }
    }
}

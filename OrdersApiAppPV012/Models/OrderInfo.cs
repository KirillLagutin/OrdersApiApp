namespace OrdersApiAppPV012.Models
{
    public class OrderInfo
    {
        public Guid Id { get; set; }

        public string OrderDescription { get; set; } = "Описание заказа";

        public Dictionary<string, int>? ProductTitleAndCount { get; set; }

        public int CountAllProducts { get; set; }
    }
}

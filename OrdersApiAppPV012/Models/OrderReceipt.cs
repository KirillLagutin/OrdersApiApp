namespace OrdersApiAppPV012.Models
{
    public class OrderReceipt
    {
        public Guid Id { get; set; }

        public List<string>? ProductTitle { get; set; }

        public List<int>? ProductCount { get; set; }

        public List<decimal>? Price { get; set; }

        public decimal TotalSum { get; set; }
    }
}

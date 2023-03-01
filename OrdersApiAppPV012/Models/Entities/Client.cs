namespace OrdersApiAppPV012.Models.Entities
{
    // сущность клиента - класс-хранилка данных о клиенте (DTO - data transfer object, data-class)
    public class Client
    {
        public Guid Id { get; init; }
        public string Name { get; set; }


        public Client()
        {
            Name = "";
        }

        public Client(Guid id, string name)
        {
            Id = id;
            Name = name;
        }


        public override string ToString()
        {
            return $"{Id} - {Name}";
        }
    }
}

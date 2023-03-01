namespace OrdersApiAppPV012.Models.Entities
{
    // сущность продукта - класс-хранилка данных о продукте (DTO - data transfer object, data-class)
    public class Product
    {
        public Guid Id { get; init; }
        public string Title { get; set; }
        public int Article { get; set; }


        public Product()
        {
            Title = "";
            Article = 0;
        }

        public Product(Guid id, string title, int article)
        {
            Id = id;
            Title = title;
            Article = article;
        }


        public override string ToString()
        {
            return $"{Id} - {Title} - {Article}";
        }
    }
}

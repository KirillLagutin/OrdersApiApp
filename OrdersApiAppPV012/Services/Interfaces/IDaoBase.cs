namespace OrdersApiAppPV012.Services.Interfaces
{
    // Базовый CRUD интерфейс
    public interface IDaoBase<T> where T : class
    {
        Task<IResult> GetAllItems();

        Task<IResult> GetItemById(Guid id);

        Task<IResult> AddItem(T entity);

        Task<IResult> UpdateItem(T entity);

        Task<IResult> DeleteItem(Guid id);
    }
}

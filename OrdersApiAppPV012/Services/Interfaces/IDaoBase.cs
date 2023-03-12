namespace OrdersApiAppPV012.Services.Interfaces
{
    // DAO (data-access-object)
    public interface IDaoBase<T> where T : class
    {
        Task<IReadOnlyList<T>> GetAllItems();

        Task<T?> GetItemById(Guid id);

        Task<T> AddItem(T entity);

        Task<T> UpdateItem(T entity);

        Task<bool> DeleteItem(Guid id);
    }
}

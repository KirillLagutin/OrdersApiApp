namespace OrdersApiAppPV012.Services.Interfaces
{
    // DAO (data-access-object)
    public interface IDaoBase<T> where T : class
    {
        Task<IResult> GetAllItems();

        Task<IResult> GetItemById(Guid id);

        Task<IResult> AddItem(T entity);

        Task<IResult> UpdateItem(T entity);

        Task<IResult> DeleteItem(Guid id);
    }
}

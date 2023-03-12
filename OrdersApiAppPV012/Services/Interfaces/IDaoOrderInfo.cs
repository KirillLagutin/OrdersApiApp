namespace OrdersApiAppPV012.Services.Interfaces
{
    // Интерфейс для инфы о заказе
    public interface IDaoOrderInfo
    {
        Task<IResult> GetOrderInfo(Guid orderId);
    }
}

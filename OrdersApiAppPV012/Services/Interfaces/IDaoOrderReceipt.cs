namespace OrdersApiAppPV012.Services.Interfaces
{
    // Интерфейс для общей суммы заказа
    public interface IDaoOrderReceipt
    {
        Task<IResult> GetOrderReceipt(Guid orderId);
    }
}

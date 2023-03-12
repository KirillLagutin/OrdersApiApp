namespace OrdersApiAppPV012.Services.Interfaces
{
    public interface IDaoOrderReceipt
    {
        Task<IResult> GetOrderReceipt(Guid orderId);
    }
}

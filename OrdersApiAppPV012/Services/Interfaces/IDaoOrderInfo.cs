using OrdersApiAppPV012.Models.Entities;

namespace OrdersApiAppPV012.Services.Interfaces
{
    public interface IDaoOrderInfo
    {
        Task<IResult> GetOrderInfo(Guid orderId);
    }
}

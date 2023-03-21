using OrdersApiAppPV012.Models;
using OrdersApiAppPV012.Services.Repositories;

namespace OrdersApiAppPV012.Services.Interfaces
{
    // Интерфейс для инфы о заказе
    public interface IDaoOrderInfo
    {
        Task<IResult> GetOrderInfo(Guid orderId);
    }
}

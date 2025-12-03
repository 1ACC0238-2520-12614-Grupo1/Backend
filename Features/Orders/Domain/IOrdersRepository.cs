namespace FuelTrack.Api.Features.Orders.Domain;

public interface IOrdersRepository
{
    Task<IEnumerable<OrderSummary>> GetOrdersAsync();
    Task<OrderDetail?> GetOrderDetailAsync(string id);
    Task<OrderDetail> CreateOrderAsync(NewOrderRequest request);
}
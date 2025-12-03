namespace FuelTrack.Api.Features.Orders.Domain;

public enum OrderStatus
{
    Created,
    Scheduled,
    OnRoute,
    Delivered,
    Cancelled
}
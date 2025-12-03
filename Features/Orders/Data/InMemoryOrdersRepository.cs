using FuelTrack.Api.Features.Orders.Domain;

namespace FuelTrack.Api.Features.Orders.Data;

public class InMemoryOrdersRepository : IOrdersRepository
{
    private readonly List<OrderDetail> _orders = new();

    public InMemoryOrdersRepository()
    {
        // Semilla de datos de ejemplo
        _orders.Add(new OrderDetail
        {
            Id = "ORD-001",
            Status = OrderStatus.OnRoute,
            Product = "Diesel B5",
            QuantityGallons = 3500,
            CreatedAt = "Creado el 01/12/2025 · 07:15",
            Eta = "Entrega estimada hoy · 10:30",
            Plant = "Planta Callao",
            Address = "Av. Industrial 123, Callao",
            TimeWindow = "Mañana (7:00 – 11:00)",
            PaymentMethod = "Tarjeta corporativa – Visa terminada en 1234",
            Amount = 48750.00
        });

        _orders.Add(new OrderDetail
        {
            Id = "ORD-002",
            Status = OrderStatus.Delivered,
            Product = "Gasohol 95",
            QuantityGallons = 2000,
            CreatedAt = "Creado el 25/11/2025 · 08:50",
            Eta = "Entregado el 25/11/2025 · 10:15",
            Plant = "Planta Lurín",
            Address = "Av. Panamericana Sur km 40, Lurín",
            TimeWindow = "Mañana (7:00 – 11:00)",
            PaymentMethod = "Tarjeta corporativa – Mastercard terminada en 5678",
            Amount = 26800.00
        });

        _orders.Add(new OrderDetail
        {
            Id = "ORD-003",
            Status = OrderStatus.Scheduled,
            Product = "Diesel B5",
            QuantityGallons = 5000,
            CreatedAt = "Creado el 30/11/2025 · 16:10",
            Eta = "Programado para 15/12/2025 · 07:30",
            Plant = "Planta Callao",
            Address = "Av. Industrial 123, Callao",
            TimeWindow = "Mañana (7:00 – 11:00)",
            PaymentMethod = null,
            Amount = null
        });
    }

    public Task<IEnumerable<OrderSummary>> GetOrdersAsync()
    {
        var summaries = _orders.Select(o => new OrderSummary
        {
            Id = o.Id,
            Status = o.Status,
            ScheduledAt = o.Eta,          // en el cliente lo muestras como fecha/hora
            PlantName = o.Plant,
            FuelType = o.Product,
            QuantityGallons = o.QuantityGallons
        });

        return Task.FromResult(summaries);
    }

    public Task<OrderDetail?> GetOrderDetailAsync(string id)
    {
        var order = _orders.FirstOrDefault(o => o.Id == id);
        return Task.FromResult(order);
    }

    public Task<OrderDetail> CreateOrderAsync(NewOrderRequest request)
    {
        var newId = $"ORD-{_orders.Count + 1:000}";

        var newOrder = new OrderDetail
        {
            Id = newId,
            Status = OrderStatus.Scheduled,
            Product = request.FuelType,
            QuantityGallons = request.QuantityGallons,
            CreatedAt = $"Creado el {DateTime.Now:dd/MM/yyyy · HH:mm}",
            Eta = "Entrega estimada pendiente",
            Plant = "Planta Callao",
            Address = request.Address,
            TimeWindow = request.TimeWindow,
            PaymentMethod = null,
            Amount = null
        };

        _orders.Add(newOrder);
        return Task.FromResult(newOrder);
    }
}

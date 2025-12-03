using FuelTrack.Api.Features.Orders.Domain;
using Microsoft.AspNetCore.Mvc;

namespace FuelTrack.Api.Features.Orders.Api;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrdersRepository _repository;

    public OrdersController(IOrdersRepository repository)
    {
        _repository = repository;
    }

    // GET /api/orders
    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderSummary>>> GetOrders()
    {
        var orders = await _repository.GetOrdersAsync();
        return Ok(orders);
    }

    // GET /api/orders/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<OrderDetail>> GetOrderDetail(string id)
    {
        var order = await _repository.GetOrderDetailAsync(id);
        if (order == null) return NotFound();
        return Ok(order);
    }

    // POST /api/orders
    [HttpPost]
    public async Task<ActionResult<OrderDetail>> CreateOrder([FromBody] NewOrderRequest request)
    {
        var created = await _repository.CreateOrderAsync(request);
        // para que en Swagger se vea bien y coincida con Retrofit:
        return CreatedAtAction(nameof(GetOrderDetail), new { id = created.Id }, created);
    }
}
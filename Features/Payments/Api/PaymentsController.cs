using FuelTrack.Api.Features.Payments.Domain;
using Microsoft.AspNetCore.Mvc;

namespace FuelTrack.Api.Features.Payments.Api;

[ApiController]
[Route("api/[controller]")]
public class PaymentsController : ControllerBase
{
    private readonly IPaymentsRepository _repository;

    public PaymentsController(IPaymentsRepository repository)
    {
        _repository = repository;
    }

    // GET api/payments/methods
    [HttpGet("methods")]
    public async Task<ActionResult<IEnumerable<PaymentMethod>>> GetPaymentMethods()
    {
        var methods = await _repository.GetPaymentMethodsAsync();
        return Ok(methods);
    }

    // POST api/payments/methods
    [HttpPost("methods")]
    public async Task<ActionResult<PaymentMethod>> AddPaymentMethod(
        [FromBody] NewPaymentMethodRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var method = await _repository.AddPaymentMethodAsync(request);
        return CreatedAtAction(nameof(GetPaymentMethods), new { id = method.Id }, method);
    }

    // GET api/payments/history
    [HttpGet("history")]
    public async Task<ActionResult<IEnumerable<PaymentHistory>>> GetPaymentHistory()
    {
        var history = await _repository.GetPaymentHistoryAsync();
        return Ok(history);
    }
}
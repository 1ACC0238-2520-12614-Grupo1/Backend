using FuelTrack.Api.Features.Payments.Domain;

namespace FuelTrack.Api.Features.Payments.Data;

public class InMemoryPaymentsRepository : IPaymentsRepository
{
    private readonly List<PaymentMethod> _methods = new();
    private readonly List<PaymentHistory> _history = new();
    private int _methodSequence = 1;
    private int _paymentSequence = 1;

    public InMemoryPaymentsRepository()
    {
        // seed de ejemplo
        var method1 = new PaymentMethod
        {
            Id = "PM-001",
            Brand = "Visa",
            Masked = "**** **** **** 1234",
            Holder = "Cliente FuelTrack SAC",
            Expires = "08/27",
            IsDefault = true
        };

        var method2 = new PaymentMethod
        {
            Id = "PM-002",
            Brand = "Mastercard",
            Masked = "**** **** **** 5678",
            Holder = "Cliente FuelTrack SAC",
            Expires = "03/26",
            IsDefault = false
        };

        _methods.Add(method1);
        _methods.Add(method2);

        _history.Add(new PaymentHistory
        {
            Id = "PAY-001",
            Date = "01/12/2025 · 08:45",
            Description = "Pedido ORD-001 · 3,500 gal Diesel B5",
            Amount = 48750.00,
            Currency = "PEN",
            Status = "Pagado"
        });

        _history.Add(new PaymentHistory
        {
            Id = "PAY-000",
            Date = "25/11/2025 · 10:20",
            Description = "Pedido ORD-002 · 2,000 gal Gasohol 95",
            Amount = 26800.00,
            Currency = "PEN",
            Status = "Pagado"
        });

        _methodSequence = _methods.Count + 1;
        _paymentSequence = _history.Count + 1;
    }

    public Task<IEnumerable<PaymentMethod>> GetPaymentMethodsAsync()
    {
        return Task.FromResult<IEnumerable<PaymentMethod>>(_methods);
    }

    public Task<PaymentMethod> AddPaymentMethodAsync(NewPaymentMethodRequest request)
    {
        var last4 = request.CardNumber.Length >= 4
            ? request.CardNumber[^4..]
            : request.CardNumber;

        var newMethod = new PaymentMethod
        {
            Id = $"PM-{_methodSequence:000}",
            Brand = request.Brand,
            Masked = $"**** **** **** {last4}",
            Holder = string.IsNullOrWhiteSpace(request.Holder)
                ? "Cliente FuelTrack SAC"
                : request.Holder,
            Expires = request.Expires,
            IsDefault = false
        };

        _methods.Add(newMethod);
        _methodSequence++;

        return Task.FromResult(newMethod);
    }

    public Task<IEnumerable<PaymentHistory>> GetPaymentHistoryAsync()
    {
        return Task.FromResult<IEnumerable<PaymentHistory>>(_history
            .OrderByDescending(h => h.Id)); // solo para variar un poco
    }
}

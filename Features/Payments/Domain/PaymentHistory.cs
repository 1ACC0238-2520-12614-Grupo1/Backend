namespace FuelTrack.Api.Features.Payments.Domain;

public class PaymentHistory
{
    public string Id { get; set; } = default!;
    public string Date { get; set; } = default!;          // "01/12/2025 · 08:45"
    public string Description { get; set; } = default!;
    public double Amount { get; set; }
    public string Currency { get; set; } = default!;      // "PEN", "USD", etc.
    public string Status { get; set; } = default!;        // "Pagado", "Pendiente"
}
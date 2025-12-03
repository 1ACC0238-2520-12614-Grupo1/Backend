namespace FuelTrack.Api.Features.Payments.Domain;

public class NewPaymentMethodRequest
{
    public string Brand { get; set; } = default!;
    public string CardNumber { get; set; } = default!;
    public string Holder { get; set; } = default!;
    public string Expires { get; set; } = default!;   // "MM/AA"
}
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FuelTrack.Api.Features.Payments.Domain;

public interface IPaymentsRepository
{
    Task<IEnumerable<PaymentMethod>> GetPaymentMethodsAsync();
    Task<PaymentMethod> AddPaymentMethodAsync(NewPaymentMethodRequest request);
    Task<IEnumerable<PaymentHistory>> GetPaymentHistoryAsync();
}
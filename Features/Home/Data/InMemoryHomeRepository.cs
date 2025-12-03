using System.Threading;
using System.Threading.Tasks;
using FuelTrack.Api.Features.Home.Domain;

namespace FuelTrack.Api.Features.Home.Data;

public class InMemoryHomeRepository : IHomeRepository
{
    public Task<DashboardSummary> GetDashboardAsync(
        string? clientId,
        CancellationToken cancellationToken = default
    )
    {
        // Datos dummy por ahora. Luego aquí lees de la BD usando el clientId.
        var dashboard = new DashboardSummary
        {
            CompanyName = "Cliente FuelTrack SAC",

            // 👇 NUEVO: si ya tienes una URL real, la pones aquí.
            // Por ahora puedes dejarla null para que el cliente muestre iniciales.
            AvatarUrl = null,
            // Ejemplo si tuvieras una imagen hosteada:
            // AvatarUrl = "https://mi-cdn.com/avatars/cliente-fueltrack.png",

            ActiveOrder = new OrderSummary
            {
                FuelType = "Diesel B5",
                QuantityGallons = 3500,
                Status = "En ruta hoy"
            },
            NextDelivery = new DeliverySummary
            {
                DateTimeText = "Hoy · 10:30 a. m.",
                Location = "Planta Callao",
                Status = "Programada"
            },
            LastPayment = new PaymentSummary
            {
                AmountText = "S/ 48,750.00",
                Method = "Tarjeta corporativa · Visa terminada en 1234",
                Status = "Pagado"
            }
        };

        return Task.FromResult(dashboard);
    }
}
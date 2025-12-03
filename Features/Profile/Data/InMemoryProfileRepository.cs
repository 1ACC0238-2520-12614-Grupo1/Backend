using FuelTrack.Api.Features.Profile.Domain;

namespace FuelTrack.Api.Features.Profile.Data;

public class InMemoryProfileRepository : IProfileRepository
{
    // guardamos 1 perfil en memoria (tipo empresa cliente)
    private ProfileInfo _profile = new()
    {
        CompanyName = "Cliente FuelTrack SAC",
        Ruc = "20123456789",
        Email = "cliente@fueltrack.com",
        Phone = "+51 999 999 999",
        ContactName = "Contacto Logística",
        AvatarUrl = null,                    // luego puedes cambiarlo
        LastPasswordChange = "Pendiente"     // o una fecha tipo "2025-12-01"
    };

    public Task<ProfileInfo> GetProfileAsync()
    {
        return Task.FromResult(_profile);
    }

    public Task<ProfileInfo> UpdateProfileAsync(ProfileInfo profile)
    {
        // aquí podrías validar campos; por ahora simplemente reemplazamos
        _profile = profile;
        return Task.FromResult(_profile);
    }
}
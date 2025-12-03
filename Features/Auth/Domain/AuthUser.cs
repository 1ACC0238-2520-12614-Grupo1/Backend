namespace FuelTrack.Api.Features.Auth.Domain;

public class AuthUser
{
    public string Id { get; set; } = default!;
    public string FullName { get; set; } = default!;
    public string Email { get; set; } = default!;
    // Demo: password en texto plano (luego lo puedes cambiar a hash)
    public string Password { get; set; } = default!;
}
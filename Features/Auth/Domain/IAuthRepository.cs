using System.Threading.Tasks;

namespace FuelTrack.Api.Features.Auth.Domain;

public interface IAuthRepository
{
    Task<AuthResult> RegisterAsync(RegisterRequest request);
    Task<AuthResult?> LoginAsync(LoginRequest request);
}
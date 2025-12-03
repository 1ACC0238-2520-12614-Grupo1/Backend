namespace FuelTrack.Api.Features.Auth.Domain;

public record AuthResult(
    string Token,
    UserDto User
);
namespace FuelTrack.Api.Features.Auth.Domain;

public record LoginRequest(
    string Email,
    string Password
);
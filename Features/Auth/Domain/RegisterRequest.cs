namespace FuelTrack.Api.Features.Auth.Domain;

public record RegisterRequest(
    string FullName,
    string Email,
    string Password
);
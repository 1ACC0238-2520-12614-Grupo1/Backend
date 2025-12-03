namespace FuelTrack.Api.Features.Auth.Domain;

public record UserDto(
    string Id,
    string FullName,
    string Email
);
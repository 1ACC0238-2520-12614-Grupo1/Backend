using System.Threading.Tasks;

namespace FuelTrack.Api.Features.Profile.Domain;

public interface IProfileRepository
{
    Task<ProfileInfo> GetProfileAsync();
    Task<ProfileInfo> UpdateProfileAsync(ProfileInfo profile);
}
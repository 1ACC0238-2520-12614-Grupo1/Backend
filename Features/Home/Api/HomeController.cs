using System.Threading;
using System.Threading.Tasks;
using FuelTrack.Api.Features.Home.Domain;
using Microsoft.AspNetCore.Mvc;

namespace FuelTrack.Api.Features.Home.Api;

[ApiController]
[Route("api/client")]
public class HomeController : ControllerBase
{
    private readonly IHomeRepository _homeRepository;

    public HomeController(IHomeRepository homeRepository)
    {
        _homeRepository = homeRepository;
    }

    // Coincide con tu HomeApi de Android: @GET("client/dashboard")
    [HttpGet("dashboard")]
    public async Task<ActionResult<DashboardSummary>> GetDashboard(
        CancellationToken cancellationToken
    )
    {
        var dashboard = await _homeRepository.GetDashboardAsync(
            clientId: null,
            cancellationToken: cancellationToken
        );

        return Ok(dashboard);
    }

}
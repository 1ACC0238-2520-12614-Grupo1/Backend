using FuelTrack.Api.Features.Auth.Domain;
using Microsoft.AspNetCore.Mvc;

namespace FuelTrack.Api.Features.Auth.Api;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthRepository _repository;

    public AuthController(IAuthRepository repository)
    {
        _repository = repository;
    }

    // POST api/auth/register
    [HttpPost("register")]
    public async Task<ActionResult<AuthResult>> Register([FromBody] RegisterRequest request)
    {
        try
        {
            var result = await _repository.RegisterAsync(request);
            return Ok(result);
        }
        catch (InvalidOperationException ex) when (ex.Message == "EMAIL_ALREADY_EXISTS")
        {
            return BadRequest(new { message = "El correo ya está registrado" });
        }
    }

    // POST api/auth/login
    [HttpPost("login")]
    public async Task<ActionResult<AuthResult>> Login([FromBody] LoginRequest request)
    {
        var result = await _repository.LoginAsync(request);

        if (result is null)
        {
            return Unauthorized();
        }

        return Ok(result);
    }
}
using FuelTrack.Api.Features.Profile.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;

namespace FuelTrack.Api.Features.Profile.Api;

[ApiController]
[Route("api/[controller]")]
public class ProfileController : ControllerBase
{
    private readonly IProfileRepository _repository;
    private readonly IWebHostEnvironment _env;

    public ProfileController(IProfileRepository repository, IWebHostEnvironment env)
    {
        _repository = repository;
        _env = env;
    }

    // GET api/profile/me
    [HttpGet("me")]
    public async Task<ActionResult<ProfileInfo>> GetMe()
    {
        var profile = await _repository.GetProfileAsync();
        return Ok(profile);
    }

    // PUT api/profile/me
    [HttpPut("me")]
    public async Task<ActionResult<ProfileInfo>> UpdateMe([FromBody] ProfileInfo request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updated = await _repository.UpdateProfileAsync(request);
        return Ok(updated);
    }

    // POST api/profile/avatar
    // Content-Type: multipart/form-data
    [HttpPost("avatar")]
    public async Task<ActionResult<ProfileInfo>> UploadAvatar(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("File is required.");

        // Asegurar carpeta wwwroot/avatars
        var webRoot = _env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        var avatarsPath = Path.Combine(webRoot, "avatars");

        if (!Directory.Exists(avatarsPath))
            Directory.CreateDirectory(avatarsPath);

        // Nombre único
        var extension = Path.GetExtension(file.FileName);
        var fileName = $"avatar-{Guid.NewGuid():N}{extension}";
        var filePath = Path.Combine(avatarsPath, fileName);

        await using (var stream = System.IO.File.Create(filePath))
        {
            await file.CopyToAsync(stream);
        }

        // Ruta relativa que usará el cliente: http://host:port + avatarUrl
        var relativePath = $"/avatars/{fileName}";

        // Actualizar perfil con la nueva URL
        var profile = await _repository.GetProfileAsync();
        profile.AvatarUrl = relativePath;

        var updated = await _repository.UpdateProfileAsync(profile);

        return Ok(updated);
    }
}

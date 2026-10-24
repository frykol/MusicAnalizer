using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using backend.Dtos.Artist;
using backend.Services.Results;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using backend.Extensions;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private readonly IArtistService _service;
        private readonly IAuthService _authService;

        public ArtistController(IArtistService service, IAuthService authService)
        {
            _service = service;
            _authService = authService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ArtistResponseDto>>> GetAll()
        {
            var artists = await _service.GetAllAsync();
            return Ok(artists);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ArtistDetailedResponseDto>> GetById(int id)
        {
            var artist = await _service.GetByIdAsync(id);
            if (artist is null)
                return NotFound();
            return Ok(artist);
        }

        [Authorize]
        [HttpPut("{id:int}")]
        public async Task<ActionResult<ArtistDetailedResponseDto>> Update(int id, ArtistUpdateDto updateDto)
        {
            int? userId = User.GetUserId();

            var artist = await _service.UpdateAsync(id, updateDto, userId ?? default);
            if (artist.Status == ArtistServiceStatus.ArtistNotFound)
                return NotFound();

            if (artist.Status == ArtistServiceStatus.InvalidCredentials)
                return Unauthorized();

            return Ok(artist.Value);    
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ArtistResponseDto>> Create(ArtistCreateDto createDto)
        {
            int? userId = User.GetUserId();

            var user = await _authService.GetByIdAsync(userId);

            if (user is null)
                return NotFound("User not found");

            var artist = await _service.CreateAsync(createDto, user);
            return CreatedAtAction(nameof(GetById), new {id = artist.Id}, artist);   
        }

        [Authorize]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            int? userId = User.GetUserId();

            DeleteArtistResult result = await _service.DeleteAsync(id, userId ?? default);
            return result switch
            {
                DeleteArtistResult.Success => NoContent(),

                DeleteArtistResult.NotFound => NotFound("Artist not found"),

                DeleteArtistResult.HasTracks => Conflict("Artist has assigned tracks"),

                DeleteArtistResult.InvalidPermission => Unauthorized("You don't have permission to delete this artist"),

                _ => StatusCode(500)
            };
        }
    }
}

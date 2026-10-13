using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using backend.Dtos.Artist;
using backend.Services.Results;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private readonly IArtistService _service;

        public ArtistController(IArtistService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<ArtistDetailedResponseDto>>> GetAll()
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
        [HttpPut("{id:int}")]
        public async Task<ActionResult<ArtistDetailedResponseDto>> Update(int id, ArtistUpdateDto updateDto)
        {
            var artist = await _service.UpdateAsync(id, updateDto);
            if (artist is null)
                return NotFound();
            return Ok(artist);    
        }
        [HttpPost]
        public async Task<ActionResult<ArtistResponseDto>> Create(ArtistCreateDto createDto)
        {
            var artist = await _service.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetById), new {id = artist.Id}, artist);   
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            DeleteArtistResult result = await _service.DeleteAsync(id);
            return result switch
            {
                DeleteArtistResult.Success => NoContent(),

                DeleteArtistResult.NotFound => NotFound("Artist not found"),

                DeleteArtistResult.HasTracks => Conflict("Artist has assigned tracks"),

                _ => StatusCode(500)
            };
        }
    }
}

using backend.Dtos;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrackController : ControllerBase
    {
        private readonly ITrackService _service;

        public TrackController(ITrackService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<Track>>> GetTracks()
        {
            var tracks = await _service.GetAllAsync();
            return Ok(tracks);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Track>> GetTrackById(int id)
        {
            var track = await _service.GetByIdAsync(id);

            if (track is null)
                return NotFound();

            return Ok(track);
        }

        [HttpPost]
        public async Task<ActionResult<Track>> CreateTrack([FromBody] TrackCreateDto createDto)
        {
            var track = await _service.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetTrackById), new {id = track.Id}, track);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Track>> UpdateTrack(int id, [FromBody] TrackUpdateDto updateDto)
        {
            var track = await _service.UpdateAsync(id, updateDto);
            if (track is null)
                return NotFound();
            return Ok(track);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteTrack(int id)
        {
            var isDeleted = await _service.DeleteAsync(id);
            if(!isDeleted)
                return NotFound();
            return NoContent();
        }
    }
}

using System.Diagnostics;
using backend.Dtos;
using backend.Dtos.Track;
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
        private readonly IAudioFileService _audioFileService;

        public TrackController(ITrackService service, IAudioFileService audioFileService)
        {
            _service = service;
            _audioFileService = audioFileService;
        }

        [HttpGet]
        public async Task<ActionResult<List<TrackResponseDto>>> GetTracks()
        {
            var tracks = await _service.GetAllAsync();
            return Ok(tracks);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TrackDetailedResponseDto>> GetTrackById(int id)
        {
            var track = await _service.GetByIdAsync(id);

            if (track is null)
                return NotFound();

            return Ok(track);
        }

        [HttpPost]
        public async Task<ActionResult<TrackResponseDto>> CreateTrack([FromBody] TrackCreateDto createDto)
        {
            var track = await _service.CreateAsync(createDto);
            if (track is null)
                return NotFound("Artist not found");
            return CreatedAtAction(nameof(GetTrackById), new {id = track.id}, track);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<TrackResponseDto>> UpdateTrack(int id, [FromBody] TrackUpdateDto updateDto)
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

        [HttpPost("file")]
        public async Task<IActionResult> UploadFile([FromForm] IFormFile file)
        {            
            var fileName = await _audioFileService.SaveAsync(file);
            return Ok(fileName);
        }
    }
}

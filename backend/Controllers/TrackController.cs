using System.Diagnostics;
using backend.Dtos;
using backend.Dtos.Track;
using backend.Extensions;
using backend.Models;
using backend.Services;
using backend.Services.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrackController : ControllerBase
    {
        private readonly ITrackService _service;
        private readonly IAudioFileService _audioFileService;
        private readonly IAuthService _authService;

        public TrackController(ITrackService service, IAudioFileService audioFileService, IAuthService authService)
        {
            _service = service;
            _audioFileService = audioFileService;
            _authService = authService;
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

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<TrackResponseDto>> CreateTrack([FromBody] TrackCreateDto createDto)
        {
            int? userId = User.GetUserId();

            var user = await _authService.GetByIdAsync(userId);

            if (user is null)
                return Unauthorized();

            var track = await _service.CreateAsync(createDto, user);
            if (track is null)
                return NotFound("Artist not found");
            return CreatedAtAction(nameof(GetTrackById), new {id = track.id}, track);
        }

        [Authorize]
        [HttpPut("{id:int}")]
        public async Task<ActionResult<TrackResponseDto>> UpdateTrack(int id, [FromBody] TrackUpdateDto updateDto)
        {
            int? userId = User.GetUserId();
            var track = await _service.UpdateAsync(id, updateDto, userId ?? default);
            if (track is null)
                return NotFound();
            return Ok(track);
        }

        [Authorize]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteTrack(int id)
        {
            
            int? userId = User.GetUserId();
            var isDeleted = await _service.DeleteAsync(id, userId ?? default);
            if(!isDeleted)
                return NotFound();
            return NoContent();
        }

        [Authorize]
        [HttpPost("{id:int}/file")]
        public async Task<IActionResult> UploadFile(int id, [FromForm] IFormFile file)
        {         
            int? userId = User.GetUserId();

            var user = await _authService.GetByIdAsync(userId);

            if (user is null)
                return Unauthorized();

            var audioFileServiceResult = await _audioFileService.SaveAsync(id, file, user);
            return audioFileServiceResult switch
            {
                AudioFileServiceResult.IsSucces => Ok("File uploaded successfully"),
                AudioFileServiceResult.IsNotMusicFile => BadRequest("The uploaded file is not a valid music file"),
                AudioFileServiceResult.IsTooLong => BadRequest("The uploaded file is too long"),
                AudioFileServiceResult.IsTooShort => BadRequest("The uploaded file is too short"),
                AudioFileServiceResult.InvalidMetadata => BadRequest("The uploaded file has invalid metadata"),
                AudioFileServiceResult.TrackNotFound => NotFound("Track not found"),
                AudioFileServiceResult.FileAlreadyExists => BadRequest("File uploaded already exists"),
                _ => StatusCode(500, "An unexpected error occurred")
            };
        }
    }
}

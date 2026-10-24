using System.Diagnostics;
using backend.Dtos;
using backend.Dtos.Track;
using backend.Extensions;
using backend.Models;
using backend.Services;
using backend.Services.Results;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
            if (track.Status == TrackServiceStatus.TrackNotFound)
                return NotFound();

            if (track.Status == TrackServiceStatus.InvalidCredentials)
                return Unauthorized();

            return Ok(track.Value);
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
                AudioFileServiceStatus.IsSucces => Ok("File uploaded successfully"),
                AudioFileServiceStatus.IsNotMusicFile => BadRequest("The uploaded file is not a valid music file"),
                AudioFileServiceStatus.IsTooLong => BadRequest("The uploaded file is too long"),
                AudioFileServiceStatus.IsTooShort => BadRequest("The uploaded file is too short"),
                AudioFileServiceStatus.InvalidMetadata => BadRequest("The uploaded file has invalid metadata"),
                AudioFileServiceStatus.TrackNotFound => NotFound("Track not found"),
                AudioFileServiceStatus.FileAlreadyExists => BadRequest("File uploaded already exists"),
                AudioFileServiceStatus.InvalidCredentials => Forbid(),
                _ => StatusCode(500, "An unexpected error occurred")
            };
        }
    }
}

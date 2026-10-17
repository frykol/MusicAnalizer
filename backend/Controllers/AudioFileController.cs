using backend.Dtos.AudioFile;
using backend.Services.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AudioFileController : ControllerBase
    {
        private readonly IAudioFileService _service;

        public AudioFileController(IAudioFileService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<AudioFileResponseDto>>> GetAll()
        {
            var audioFiles = await _service.GetAllAsync();
            return Ok(audioFiles);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<AudioFileResponseDto>> GetById(int id)
        {
            var audioFile = await _service.GetByIdAsync(id);
            if (audioFile is null)
                return NotFound("Audio file not found");
            return Ok(audioFile);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<AudioFileResponseDto>> DeleteById(int id)
        {
            var isDeleted = await _service.DeleteByIdAsync(id);
            if (!isDeleted)
                return NotFound("Audio file not found");
            return NoContent();
        }
    }
}

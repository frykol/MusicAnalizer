using backend.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrackController : ControllerBase
    {
        private readonly List<TrackDto> tracks = [
        new (1, "Seima", "Metal"),  
        new (2, "dipa", "Pop"),  
        new (3, "Sasdeima", "Rock"),  
        ];

        [HttpGet]
        public ActionResult<IEnumerable<TrackDto>> GetTracks()
        {
            return Ok(tracks);
        }
    }
}

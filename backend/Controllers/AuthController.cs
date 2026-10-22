using System.Security.Claims;
using backend.Core.Auth;
using backend.Dtos.User;
using backend.Infrastructure.Auth;
using backend.Models;
using backend.Services;
using backend.Services.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly string _secretKey;
        private readonly IUserService _service;

        public AuthController(IConfiguration configuration,IUserService service)
        {
            _service = service;
            _secretKey = configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT key missing");
        }

        [HttpPost("register")]
        public async Task<ActionResult<string>> register([FromBody] UserCreateDto userCreate)
        {
            var response = await _service.CreateUserAsync(userCreate);

            if(response.Status != UserServiceStatusResponse.Ok)
                return BadRequest("HOLAA");

            if(response.Value is null)
                return BadRequest("ERROR USER");

            var token = JwtTokenGenerator.GenerateToken(_secretKey, response.Value.Id);

            return Ok(token);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> login([FromBody] UserLoginDto userLogin)
        {
            var response = await _service.LoginAsync(userLogin);

            //TODO: Change this, quick test only
            if (response.Status != UserServiceStatusResponse.Ok)
                return BadRequest("something wrong");
                
            if(response.Value is null)
                throw new Exception("OYOY");

            var token = JwtTokenGenerator.GenerateToken(_secretKey, response.Value.Id);

            return Ok(token);
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<ActionResult<User>> me()
        {   
            int? userId = null;

            if(User.Identity?.IsAuthenticated == true)
            {
                var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if(int.TryParse(claim, out int parsedId))
                    userId = parsedId;
            }

            var user = await _service.GetByIdAsync(userId);

            if(user is null)
                return NotFound("User not found");


            return Ok(user);
        }
    }
}

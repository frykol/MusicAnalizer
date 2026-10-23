using System.Security.Claims;
using backend.Core.Auth;
using backend.Dtos.RefreshToken;
using backend.Dtos.User;
using backend.Extensions;
using backend.Infrastructure.Auth;
using backend.Mappers;
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
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserLoginResponseDto>> register([FromBody] UserCreateDto userCreate)
        {
            var response = await _service.CreateUserAsync(userCreate);

            if(response.Status != UserServiceStatusResponse.Ok)
                return BadRequest("HOLAA");

            if(response.Value is null)
                return BadRequest("ERROR USER");


            return Ok(response.Value);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserLoginResponseDto>> login([FromBody] UserLoginDto userLogin)
        {
            var response = await _service.LoginAsync(userLogin);

            //TODO: Change this, quick test only
            if (response.Status != UserServiceStatusResponse.Ok)
                return BadRequest("something wrong");
                
            if(response.Value is null)
                throw new Exception("OYOY");

            return Ok(response.Value);
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<ActionResult<UserDetailedResponseDto>> me()
        {   
            int? userId = User.GetUserId();

            var user = await _service.GetByIdAsync(userId);

            if(user is null)
                return NotFound("User not found");

            var mapped = UserMapper.toDetailedResponseDto(user);

            return Ok(mapped);
        }

        [HttpPost("access")]
        public async Task<ActionResult<string>> GetAccessTokenAsync([FromBody] RefreshTokenDto refreshTokenDto)
        {
            var token = await _service.GetAccessTokenAsync(refreshTokenDto.token);
            if (token is null)
                return Unauthorized();
            return Ok(token);    
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> LogoutAsync()
        {
            int? userId = User.GetUserId();

            await _service.LogoutAsync(userId ?? default);

            return Ok();
        }
    }
}

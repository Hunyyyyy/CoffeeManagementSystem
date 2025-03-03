using Application__CaféManagementSystem.Application_.DTOs.Users;
using Application__CaféManagementSystem.Application_.Interface;
using Application__CaféManagementSystem.Application_.Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("login")]
        public async Task <IActionResult> Login([FromBody] UserDto user)
        {
            var response = await _authService.LoginAsync(user);
            if(response==null)
                return  BadRequest("Đăng nhập thất bại");
           return Ok(response);
        }
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken()
        {
            var newAccessToken = await _authService.RefreshTokenAsync();

            if (newAccessToken == null)
            {
                return Unauthorized(new { message = "Refresh Token không hợp lệ hoặc đã hết hạn" });
            }

            return Ok(new { accessToken = newAccessToken });
        }
    }
}

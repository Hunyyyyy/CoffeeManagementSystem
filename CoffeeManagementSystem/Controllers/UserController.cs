using Application__CaféManagementSystem.Application_.DTOs.Users;
using Application__CaféManagementSystem.Application_.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [Authorize]
        [HttpPost("add")]
        public async Task<IActionResult> AddNewUser([FromBody] CreateUserDto createUserDto)
        {
            if (createUserDto == null) 
                return BadRequest("User data is required");
            var response = await _userService.AddNewUser(createUserDto);
            return Ok(response);
        }
        [Authorize]
        [HttpGet("get")]
        public async Task<IActionResult> GetAllUser()
        {
            var userList = await _userService.GetAllUserAsync();
            if (userList == null)
                return BadRequest("No user found");
            return Ok(userList);
        }
    }
}

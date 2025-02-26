using Application__CaféManagementSystem.Application_.DTOs.Salary;
using Application__CaféManagementSystem.Application_.Interface;
using Application__CaféManagementSystem.Application_.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CoffeeManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SalaryController : Controller
    {
        private readonly ISalaryService _salaryService;
        private readonly IAuthService _authService;
        public SalaryController(ISalaryService salaryService,IAuthService authService)
        {
            _salaryService = salaryService;
            _authService = authService;
        }
        
        [HttpPost("add")]
        public async Task<IActionResult> CreateSalaryAsync([FromBody] CreateSalaryDto salaryDto)
        {
            if (salaryDto == null) 
                return BadRequest("Salary is null");
            var roleId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            if (roleId == null)
            {
                return Forbid();
            }
            var roleName = await _authService.GetRoleNameByIdAsync(int.Parse(roleId));
            if (roleName.Equals("Admin") && roleName.Equals("Quản lý"))
            {
                return Forbid();
            }
            var result = await _salaryService.CreateSalaryAsync(salaryDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateSalaryAsync([FromBody] UpdateSalaryDto salaryDto)
        {
            if (salaryDto == null)
                return BadRequest("Salary is null");
            var roleId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            if (roleId == null)
            {
                return Forbid();
            }
            var roleName = await _authService.GetRoleNameByIdAsync(int.Parse(roleId));
            if (roleName.Equals("Admin") && roleName.Equals("Quản lý"))
            {
                return Forbid();
            }
            var result = await _salaryService.UpdateSalaryAsync(salaryDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("get")]
        public async Task<IActionResult> GetAllSalaryAsync()
        {
            var roleId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            if (roleId == null)
            {
                return Forbid();
            }
            var roleName = await _authService.GetRoleNameByIdAsync(int.Parse(roleId));
            if (roleName.Equals("Admin") && roleName.Equals("Quản lý"))
            {
                return Forbid();
            }
            var result = await _salaryService.GetAllSalaryAsync();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}

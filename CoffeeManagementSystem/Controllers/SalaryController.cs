using Application__CaféManagementSystem.Application_.DTOs.Salary;
using Application__CaféManagementSystem.Application_.Interface;
using Application__CaféManagementSystem.Application_.Services;
using Core_CaféManagementSystem.Core.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CoffeeManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = nameof(Enums.Role.Manager))]
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

            var result = await _salaryService.CreateSalaryAsync(salaryDto);
            return Ok(result);
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateSalaryAsync([FromBody] UpdateSalaryDto salaryDto)
        {
            if (salaryDto == null)
                return BadRequest("Salary is null");
          
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
           
            var result = await _salaryService.GetAllSalaryAsync();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}

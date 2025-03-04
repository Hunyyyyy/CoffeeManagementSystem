using Application__CaféManagementSystem.Application_.DTOs.Employees;
using Application__CaféManagementSystem.Application_.Interface;
using Azure.Core;
using Core_CaféManagementSystem.Core.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CoffeeManagementSystem.Controllers
{
    [ApiController]
    [Authorize(Policy = nameof(Enums.Role.Manager))]
    [Route("api/[Controller]")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IAuthService _authService;
        public EmployeeController(IEmployeeService employeeService, IAuthService authService)
        {
            _employeeService = employeeService;
            _authService = authService;
        }
        [HttpGet("get")]
        public async Task<IActionResult> GetAllEmpoloyee()
        {
          
            var employeeList = await _employeeService.GetAllEmployeeAsync();
            return Ok(employeeList);
        }
        [HttpPost("add")]
        public async Task<IActionResult> AddEmployee([FromBody]CreateEmployeeDto employeeDto)
        {
            
            if (employeeDto == null)
            {
                return BadRequest("Dữ liệu đầu vào không hợp lệ.");
            }
            var employee = await _employeeService.AddEmployeeAsync(employeeDto);
            if (!employee.Success)
            {
                return BadRequest(employee);
            }
            return Ok(employee);
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateEmployee([FromBody] UpdateEmployeeDto employeeDto)
        {
          
            if (employeeDto == null)
            {
                return BadRequest("Dữ liệu đầu vào không hợp lệ.");
            }
            var employee = await _employeeService.UpdateEmployeeAsync(employeeDto.Id,employeeDto);
            if (!employee.Success)
            {
                return BadRequest(employee);
            }
            return Ok(employee);
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
           
            var employee = await _employeeService.DeleteEmployeeAsync(id);
            if (!employee.Success)
            {
                return BadRequest(employee);
            }
            return Ok(employee);
        }
        [HttpGet("search")]
        public async Task<IActionResult> SearchEmployees([FromBody] EmployeeSearchRequestDto request)
        {
           
            var query = await _employeeService.SearchEmployeesAsync(request);

            if (query == null)
            {
                return BadRequest("Dữ liệu đầu vào không hợp lệ.");
            }
            return Ok(query);
        }

        [HttpPut("change-active/{id}")]
        public async Task<IActionResult> ChangeIsActiveEmployee(int id, bool isActive)
        {
          
            var employee = await _employeeService.ChangeIsActiveEmployeeAsync(id, isActive);
            if (!employee.Success)
            {
                return BadRequest(employee);
            }
            return Ok(employee);
        }

    }
}

using Application__CaféManagementSystem.Application_.DTOs.Employees;
using Application__CaféManagementSystem.Application_.Interface;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CoffeeManagementSystem.Controllers
{
    [ApiController]
    [Route("api/employee")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IAuthService _authService;
        public EmployeeController(IEmployeeService employeeService, IAuthService authService)
        {
            _employeeService = employeeService;
            _authService = authService;
        }
        [Authorize]
        [HttpGet("get")]
        public async Task<IActionResult> GetAllEmpoloyee()
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
            var employeeList = await _employeeService.GetAllEmployeeAsync();
            return Ok(employeeList);
        }
        [Authorize]
        [HttpPost("add")]
        public async Task<IActionResult> AddEmployee([FromBody]CreateEmployeeDto employeeDto)
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
        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateEmployee([FromBody] UpdateEmployeeDto employeeDto)
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
        [Authorize]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
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
            var employee = await _employeeService.DeleteEmployeeAsync(id);
            if (!employee.Success)
            {
                return BadRequest(employee);
            }
            return Ok(employee);
        }
        [Authorize]
        [HttpGet("search")]
        public async Task<IActionResult> SearchEmployees([FromBody] EmployeeSearchRequestDto request)
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
            var query = await _employeeService.SearchEmployeesAsync(request);

            if (query == null)
            {
                return BadRequest("Dữ liệu đầu vào không hợp lệ.");
            }
            return Ok(query);
        }
        
        [Authorize]
        [HttpPut("change-active/{id}")]
        public async Task<IActionResult> ChangeIsActiveEmployee(int id, bool isActive)
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
            var employee = await _employeeService.ChangeIsActiveEmployeeAsync(id, isActive);
            if (!employee.Success)
            {
                return BadRequest(employee);
            }
            return Ok(employee);
        }

    }
}

using Application__CaféManagementSystem.Application_.DTOs.Invoices;
using Application__CaféManagementSystem.Application_.DTOs.Orders;
using Application__CaféManagementSystem.Application_.Interface;
using Application__CaféManagementSystem.Application_.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;
using static Core_CaféManagementSystem.Core.Common.Enums;

namespace CoffeeManagementSystem.Controllers
{
   // [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IAuthService _authService;
        private readonly ILogger<OrderController> _logger;
        public OrderController(IOrderService orderService, ILogger<OrderController> logger, IAuthService authService)
        {
            _orderService = orderService;
            _logger = logger;
            _authService = authService;
        }
        [HttpPost("confirm")]
        public async Task<IActionResult> CreateOrder([FromBody] EmployeeConfirmOrderDto orderDto)
        {
            var roleId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            if (roleId == null)
            {
                return Forbid(); // Trả về 403 nếu không có quyền
            }
            var roleName = await _authService.GetRoleNameByIdAsync(int.Parse(roleId));
            if (!roleName.Equals("Nhân viên thu ngân") || !roleName.Equals("Quản lý"))
            {
                return Forbid(); // Trả về 403 nếu không có quyền
            }
            if (orderDto == null)
            {
                return BadRequest("Dữ liệu đầu vào không hợp lệ.");
            }
            _logger.LogInformation($"Nhận order từ client: {JsonSerializer.Serialize(orderDto)}");

            var order = await _orderService.ConfirmOrderAsync(orderDto);
            _logger.LogInformation($"Nhận order từ client: {JsonSerializer.Serialize(order)}");
            return Ok(order);
        }

        [HttpGet("Status")]
        public async Task<IActionResult> GetStatusOrders([FromQuery] OrderStatus status)
        {
            var roleId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            if (roleId == null)
            {
                return Forbid(); // Trả về 403 nếu không có quyền
            }
            var roleName = await _authService.GetRoleNameByIdAsync(int.Parse(roleId));
            if (!roleName.Equals("Nhân viên thu ngân") || !roleName.Equals("Quản lý"))
            {
                return Forbid(); // Trả về 403 nếu không có quyền
            }
            var orders = await _orderService.GetStatusOrdersAsync(status);
            return Ok(orders);
        }


        [HttpGet("get")]
        public async Task<IActionResult> GetOrderById([FromQuery]int id)
        {
            var roleId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            if (roleId == null)
            {
                return Forbid(); // Trả về 403 nếu không có quyền
            }
            var roleName = await _authService.GetRoleNameByIdAsync(int.Parse(roleId));
            if (!roleName.Equals("Nhân viên thu ngân") || !roleName.Equals("Quản lý"))
            {
                return Forbid(); // Trả về 403 nếu không có quyền
            }
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null) return NotFound();
            return Ok(order);
        }
        [HttpPost("add")]
        public async Task<IActionResult> SendOrderFromCustomerToEmployee([FromBody] OrderCreateDto orderCreateDto)
        {

            if (orderCreateDto == null)
            {
                return BadRequest("Dữ liệu đầu vào không hợp lệ.");
            }
            var order = await _orderService.SendOrderFromCustomerToEmployee(orderCreateDto);
            return Ok(order);
        }
        [HttpPatch("cancelOrder")]
        public async Task<IActionResult> CancelOrder([FromQuery] int id)
        {
            var roleId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            if (roleId == null)
            {
                return Forbid(); // Trả về 403 nếu không có quyền
            }
            var roleName = await _authService.GetRoleNameByIdAsync(int.Parse(roleId));
            if (!roleName.Equals("Nhân viên thu ngân") || !roleName.Equals("Quản lý"))
            {
                return Forbid(); // Trả về 403 nếu không có quyền
            }
            var result = await _orderService.CancelOrderAsync(id);
            if (result.Data)
            {
                return Ok(result);
            }
            return BadRequest("Hủy đơn hàng thất bại");
        }
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllOrders()
        {
            var roleId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            if (roleId == null)
            {
                return Forbid(); // Trả về 403 nếu không có quyền
            }
            var roleName = await _authService.GetRoleNameByIdAsync(int.Parse(roleId));
            if (!roleName.Equals("Nhân viên thu ngân") || !roleName.Equals("Quản lý"))
            {
                return Forbid(); // Trả về 403 nếu không có quyền
            }
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }
    }
}

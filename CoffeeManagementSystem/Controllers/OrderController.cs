using Application__CaféManagementSystem.Application_.DTOs.Invoices;
using Application__CaféManagementSystem.Application_.DTOs.Orders;
using Application__CaféManagementSystem.Application_.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CoffeeManagementSystem.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<OrderController> _logger;
        public OrderController(IOrderService orderService, ILogger<OrderController> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderCreateDto orderDto)
        {
            if (orderDto == null)
            {
                return BadRequest("Dữ liệu đầu vào không hợp lệ.");
            }
            _logger.LogInformation($"Nhận order từ client: {JsonSerializer.Serialize(orderDto)}");

            var order = await _orderService.CreateOrderAsync(orderDto);
            _logger.LogInformation($"Nhận order từ client: {JsonSerializer.Serialize(order)}");
            return Ok(order);
        }
        [HttpGet("Status")]
        public async Task<IActionResult> GetStatusOrders()
        {
            var orders = await _orderService.GetStatusOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null) return NotFound();
            return Ok(order);
        }
    }
}

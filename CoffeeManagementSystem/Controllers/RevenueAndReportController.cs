using Application__CaféManagementSystem.Application_.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeManagementSystem.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("api/revenue-and-report")]
    public class RevenueAndReportController : Controller
    {
        private readonly IRevenueAndReportService _revenueAndReportService;
        private readonly IAuthService _authService;
        public RevenueAndReportController(IRevenueAndReportService revenueAndReportService, IAuthService authService)
        {
            _revenueAndReportService = revenueAndReportService;
            _authService = authService;
        }
        [HttpGet("get-revenue-by-date")]
        public async Task<IActionResult> GetTotalRevenueByDateAsync()
        {
            var response = await _revenueAndReportService.GetTotalRevenueByDateAsync();

            return Ok(response);
        }
        [HttpGet("get-revenue-by-month")]
        public async Task<IActionResult> GetRevenueByMonthAsync()
        {
            var response = await _revenueAndReportService.GetRevenueByMonthAsync();
            return Ok(response);
        }
        [HttpGet("get-total-salary-month")]
        public async Task<IActionResult> GetTotalSalaryForCurrentMonth()
        {
            var response = await _revenueAndReportService.GetTotalSalaryForCurrentMonthAsync();
            return Ok(response);
        }
        [HttpGet("get-net-profit")]
        public async Task<IActionResult> GetNetProfit()
        {
            var response = await _revenueAndReportService.GetNetProfitAsync();
            return Ok(response);
        }
        [HttpGet("get-revenue-by-product")]
        public async Task<IActionResult> GetRevenueByProduct()
        {
            var response = await _revenueAndReportService.GetRevenueByProductAsync();
            return Ok(response);
        }
        [HttpGet("get-revenue-by-payment-method")]
        public async Task<IActionResult> GetRevenueByPaymentMethod()
        {
            var response = await _revenueAndReportService.GetRevenueByPaymentMethodAsync();
            return Ok(response);
        }
        [HttpGet("get-best-selling")]
        public async Task<IActionResult> GetTop5BestSellingProducts()
        {
            var response = await _revenueAndReportService.GetTop5BestSellingProductsAsync();
            return Ok(response);
        }
        [HttpGet("get-revenue-by-hour")]
        public async Task<IActionResult> GetRevenueByHour()
        {
            var response = await _revenueAndReportService.GetRevenueByHourAsync();
            return Ok(response);
        }

    }
}

using Application__CaféManagementSystem.Application_.DTOs.RevenueAndReportDto;
using Application__CaféManagementSystem.Application_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application__CaféManagementSystem.Application_.Interface
{
    public interface IRevenueAndReportService
    {
        //Task<decimal> GetTotalRevenueAsync();
        Task<ResponseModel<IEnumerable<RevenueResponseDto>>> GetTotalRevenueByDateAsync();
        Task<ResponseModel<IEnumerable<RevenueResponseDto>>> GetRevenueByMonthAsync();
        Task<ResponseModel<RevenueResponseDto>> GetTotalSalaryForCurrentMonthAsync();
        Task<ResponseModel<string>> GetNetProfitAsync();
        Task<ResponseModel<IEnumerable<ProductRevenueDto>>> GetRevenueByProductAsync();
        Task<ResponseModel<IEnumerable<RevenueByPaymentMethodDto>>> GetRevenueByPaymentMethodAsync();
        Task<ResponseModel<IEnumerable<ProductRevenueDto>>> GetTop5BestSellingProductsAsync();
        Task<ResponseModel<IEnumerable<RevenueResponseDto>>> GetRevenueByHourAsync();
        //Task<decimal> GetTotalRevenueByYearAsync(int year);
        //Task<decimal> GetTotalRevenueByDateRangeAsync(DateTime startDate, DateTime endDate);
    }
}

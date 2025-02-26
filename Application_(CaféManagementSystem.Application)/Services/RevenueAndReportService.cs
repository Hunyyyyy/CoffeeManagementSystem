using Application__CaféManagementSystem.Application_.DTOs.RevenueAndReportDto;
using Application__CaféManagementSystem.Application_.Helpers;
using Application__CaféManagementSystem.Application_.Interface;
using Application__CaféManagementSystem.Application_.Models;
using static Core_CaféManagementSystem.Core.Common.Helpers;
using Core_CaféManagementSystem.Core.Interface;
using Infrastructure__CaféManagementSystem.Infrastructure_.Data.UnitofWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application__CaféManagementSystem.Application_.Services
{
    public class RevenueAndReportService : IRevenueAndReportService
    {

        private readonly IUnitofWork _unitofWork;

        public RevenueAndReportService(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }
        //-- Doanh thu theo ngày
        public async Task<ResponseModel<IEnumerable<RevenueResponseDto>>> GetTotalRevenueByDateAsync()
        {
            var revenueData = await _unitofWork.Invoices.GetPaidInvoices()
    .GroupBy(i => i.InvoiceDate.Date)
    .Select(g => new
    {
        Day = g.Key.Day,
        TotalAmount = g.Sum(i => i.TotalAmount) // Chỉ tính tổng trước, chưa định dạng
    })
    .OrderByDescending(r => r.Day)
    .ToListAsync();

            // 🔹 Format dữ liệu sau khi lấy từ DB
            var response = revenueData.Select(g => new RevenueResponseDto
            {
                Day = g.Day,
                Total = FormatCurrency(g.TotalAmount) // Format tiền tệ sau khi lấy dữ liệu
            }).ToList();
            if (response == null)
            {
                return ResponseFactory.Fail<IEnumerable<RevenueResponseDto>>("No Revenue for Current Date");
            }
            return ResponseFactory.Success<IEnumerable<RevenueResponseDto>>(response, "Doanh thu theo ngày");
        }
        //-- Doanh thu theo tháng
        public async Task<ResponseModel<IEnumerable<RevenueResponseDto>>> GetRevenueByMonthAsync()
        {
            var response = await _unitofWork.Invoices.GetPaidInvoices()
                            .GroupBy(i => new { Year = i.InvoiceDate.Year, Month = i.InvoiceDate.Month })
                            .Select(g => new RevenueResponseDto
                            {
                                Year = g.Key.Year,
                                Month = g.Key.Month,
                                Total = FormatCurrency(g.Sum(i => i.TotalAmount))
                            })
                            .OrderByDescending(r => r.Year)
                            .ThenByDescending(r => r.Month)
                            .ToListAsync();
            if (response == null)
            {
                return ResponseFactory.Fail<IEnumerable<RevenueResponseDto>>("No Revenue for Current Month");
            }
            return ResponseFactory.Success<IEnumerable<RevenueResponseDto>>(response, "Doanh thu theo tháng");
          
        }

        //--Tổng Lương Đã Trả Cho Nhân Viên Trong Tháng
        public async Task<ResponseModel<RevenueResponseDto>> GetTotalSalaryForCurrentMonthAsync()
        {
            var result = await _unitofWork.Invoices.GetSalariesForCurrentMonth()
                            .GroupBy(s => new { s.Month, s.Year })
                            .Select(g => new RevenueResponseDto
                            {
             Total = FormatCurrency( g.Sum(s => s.TotalAmount)),
             Month = g.Key.Month,
             Year = g.Key.Year
         })
         .FirstOrDefaultAsync(); // Lấy kết quả duy nhất cho tháng hiện tại
            if (result == null)
            {
                return ResponseFactory.Fail<RevenueResponseDto>("No Salary for Current Month");
            }
            return ResponseFactory.Success(result, "Tổng Lương Đã Trả Cho Nhân Viên Trong Tháng");
            
        }
        //Tổng Lợi Nhuận (Doanh Thu - Chi Phí Lương)
        public async Task<ResponseModel<string>> GetNetProfitAsync()
        {
            var totalRevenue = await _unitofWork.Invoices.GetPaidInvoices().SumAsync(i => i.TotalAmount);
            var totalSalary = await _unitofWork.Invoices.GetSalariesForCurrentMonth().SumAsync(s => s.TotalAmount);
            var netProfit = FormatCurrency(totalRevenue - totalSalary);
            return ResponseFactory.Success(netProfit, "Tổng Lợi Nhuận (Doanh Thu - Chi Phí Lương)");
        }
        //-- Doanh thu theo sản phẩm
        public async Task<ResponseModel<IEnumerable<ProductRevenueDto>>> GetRevenueByProductAsync()
        {
            var revenueByProduct = await _unitofWork.Orders.GetCompletedOrderDetails()
                .Include(od => od.Product)
                .Where(od => od.Product != null) // 🔥 Đảm bảo Product không null
                .GroupBy(od => new { od.Product!.ProductId, od.Product!.ProductName }) // Dùng ! để chắc chắn không null
                .Select(g => new ProductRevenueDto
                {
                    ProductID = g.Key.ProductId,
                    ProductName = g.Key.ProductName,
                    TotalQuantitySold = g.Sum(od => od.Quantity),
                    TotalRevenue = g.Sum(od => od.Quantity * (od.UnitPrice - od.Discount)) // Giữ nguyên decimal
                })
                .Where(p => p.TotalRevenue > 0)
                .OrderByDescending(p => p.TotalRevenue)
                .ToListAsync();

            // ✅ Format tiền sau khi truy vấn xong
            var formattedRevenue = revenueByProduct.Select(p => new ProductRevenueDto
            {
                ProductID = p.ProductID,
                ProductName = p.ProductName,
                TotalQuantitySold = p.TotalQuantitySold,
                TotalRevenueFormatted = FormatCurrency(p.TotalRevenue) // Thêm property mới cho string
            });

            return ResponseFactory.Success(formattedRevenue, "Revenue by Product");
        }

        //Doanh thu theo PTTT
        public async Task<ResponseModel<IEnumerable<RevenueByPaymentMethodDto>>> GetRevenueByPaymentMethodAsync()
        {
            var result = await _unitofWork.Orders.GetCompletedOrders()
                .Join(_unitofWork.Invoices.GetAll(),
                      o => o.OrderId,
                      i => i.OrderId,
                      (o, i) => new { o, i }) // Ensure invoices are IQueryable
                .Join(_unitofWork.Payments.GetAll(),
                      oi => oi.i.InvoiceId,
                      p => p.InvoiceId,
                      (oi, p) => new { oi.o, p }) // Ensure payments are IQueryable
                .GroupBy(x => x.p.PaymentMethod)
                .Select(g => new RevenueByPaymentMethodDto
                {
                    PaymentMethod = g.Key.ToString(),
                    TotalOrders = g.Count(),
                    TotalRevenue = g.Sum(x => x.o.TotalAmount) // ✅ Giữ kiểu decimal
                })
                .OrderByDescending(r => r.TotalRevenue)
                .ToListAsync();

            // ✅ Format tiền sau khi truy vấn xong
            var formattedResult = result.Select(r => new RevenueByPaymentMethodDto
            {
                PaymentMethod = r.PaymentMethod,
                TotalOrders = r.TotalOrders,
                TotalRevenue = r.TotalRevenue, // Giữ giá trị decimal
                TotalRevenueFormatted = FormatCurrency(r.TotalRevenue) // Chuỗi VNĐ
            });

            return ResponseFactory.Success(formattedResult, "Doanh thu theo PTTT");
        }
        //Top 5 sản phẩm bán chạy nhất
        public async Task<ResponseModel<IEnumerable<ProductRevenueDto>>> GetTop5BestSellingProductsAsync()
        {
            var result = await _unitofWork.Orders.GetCompletedOrderDetails()
                .Include(od => od.Product)
                .Where(od => od.Product != null)
                .GroupBy(od => new { od.Product!.ProductId, od.Product!.ProductName })
                .Select(g => new ProductRevenueDto
                {
                    ProductID = g.Key.ProductId,
                    ProductName = g.Key.ProductName,
                    TotalQuantitySold = g.Sum(od => od.Quantity),
                    TotalRevenue = g.Sum(od => od.Quantity * (od.UnitPrice - od.Discount))
                })
                .OrderByDescending(p => p.TotalQuantitySold)
                .Take(5)
                .ToListAsync();
            // ✅ Format tiền sau khi truy vấn xong
            var formattedResult = result.Select(p => new ProductRevenueDto
            {
                ProductID = p.ProductID,
                ProductName = p.ProductName,
                TotalQuantitySold = p.TotalQuantitySold,
                TotalRevenue = p.TotalRevenue, // Giữ nguyên decimal
                TotalRevenueFormatted = FormatCurrency(p.TotalRevenue) // Thêm property mới cho string
            });
            return ResponseFactory.Success(formattedResult, "Top 5 sản phẩm bán chạy nhất");
        }
        //doanh thu theo khung giờ
        public async Task<ResponseModel<IEnumerable<RevenueResponseDto>>> GetRevenueByHourAsync()
        {
            var revenueData = await _unitofWork.Invoices.GetPaidInvoices()
                .GroupBy(i => i.InvoiceDate.Hour)
                .Select(g => new
                {
                    Hour = g.Key,
                    TotalAmount = g.Sum(i => i.TotalAmount) // Chỉ tính tổng trước, chưa định dạng
                })
                .OrderByDescending(r => r.Hour)
                .ToListAsync();
            // 🔹 Format dữ liệu sau khi lấy từ DB
            var response = revenueData.Select(g => new RevenueResponseDto
            {
                Hour = g.Hour,
                Total = FormatCurrency(g.TotalAmount) // Format tiền tệ sau khi lấy dữ liệu
            }).ToList();
            if (response == null)
            {
                return ResponseFactory.Fail<IEnumerable<RevenueResponseDto>>("No Revenue for Current Hour");
            }
            return ResponseFactory.Success<IEnumerable<RevenueResponseDto>>(response, "Doanh thu theo giờ");
        }

    }
}

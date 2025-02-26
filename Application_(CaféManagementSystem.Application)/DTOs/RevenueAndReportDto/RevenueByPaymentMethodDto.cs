using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application__CaféManagementSystem.Application_.DTOs.RevenueAndReportDto
{
    public class RevenueByPaymentMethodDto
    {
        public required string PaymentMethod { get; set; }
        public int TotalOrders { get; set; }
        public decimal TotalRevenue { get; set; }
        public string? TotalRevenueFormatted { get; set; }
    }
}

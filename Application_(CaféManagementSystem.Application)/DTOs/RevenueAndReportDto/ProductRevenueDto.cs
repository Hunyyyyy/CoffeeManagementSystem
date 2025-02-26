using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application__CaféManagementSystem.Application_.DTOs.RevenueAndReportDto
{
    public class ProductRevenueDto
    {
        public int ProductID { get; set; }
        public string? ProductName { get; set; }
        public int TotalQuantitySold { get; set; }
        public decimal TotalRevenue { get; set; } 
        public string? TotalRevenueFormatted { get; set; } 
    }

}

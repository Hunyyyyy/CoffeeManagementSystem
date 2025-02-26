using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application__CaféManagementSystem.Application_.DTOs.RevenueAndReportDto
{
    public class RevenueResponseDto
    {
        public int? Hour { get; set; }
        public int? Year { get; set; }
        public int? Month { get; set; }
        public int? Day { get; set; }
        public string? Total { get; set; }
    }
}

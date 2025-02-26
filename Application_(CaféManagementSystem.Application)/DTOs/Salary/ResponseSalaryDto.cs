using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application__CaféManagementSystem.Application_.DTOs.Salary
{
    public class ResponseSalaryDto
    {
        public int SalaryId { get; set; }
        public int EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public string? RoleName { get; set; }
        public decimal BaseSalary { get; set; }
        public byte Month { get; set; }
        public int Year { get; set; }
        public decimal Bonus { get; set; }
        public decimal TotalSalary { get; set; }
    }
}

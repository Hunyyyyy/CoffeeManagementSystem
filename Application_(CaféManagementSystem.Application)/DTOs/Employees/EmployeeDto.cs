using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application__CaféManagementSystem.Application_.DTOs.Employees
{
    public class EmployeeDto
    {
        public int EmployeeId { get; set; }
        public string? FullName { get; set; }
        public string? Position { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public DateTime? HireDay { get; set; }
        public bool  IsActive { get; set; }
    }
}

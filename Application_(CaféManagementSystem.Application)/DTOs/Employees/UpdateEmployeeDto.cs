using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application__CaféManagementSystem.Application_.DTOs.Employees
{
    public class UpdateEmployeeDto
    {
        public int Id { get; set; }
        public required string FullName { get; set; }
        public required string Position { get; set; }
        public string? Email { get; set; }
        public required string Phone { get; set; }
        public DateTime? HireDay { get; set; }
        public required int IsActive { get; set; }
    }
}

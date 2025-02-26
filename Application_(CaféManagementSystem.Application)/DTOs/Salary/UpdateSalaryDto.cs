using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application__CaféManagementSystem.Application_.DTOs.Salary
{
    public class UpdateSalaryDto
    {
        public int SalaryId { get; set; }
        public decimal NewBaseSalary { get; set; }
        public decimal NewBonus { get; set; }
    }
}

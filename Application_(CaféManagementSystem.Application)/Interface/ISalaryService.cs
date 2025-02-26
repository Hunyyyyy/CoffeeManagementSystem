using Application__CaféManagementSystem.Application_.DTOs.Salary;
using Application__CaféManagementSystem.Application_.Models;
using Core_CaféManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application__CaféManagementSystem.Application_.Interface
{
    public interface ISalaryService
    {
        Task<ResponseModel<ResponseSalaryDto>> CreateSalaryAsync(CreateSalaryDto salaryDto);
        Task<ResponseModel<UpdateSalaryDto>> UpdateSalaryAsync(UpdateSalaryDto salaryDto);
        Task<ResponseModel<IEnumerable<ResponseSalaryDto>>> GetAllSalaryAsync();

    }
}

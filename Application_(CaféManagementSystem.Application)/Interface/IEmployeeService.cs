using Application__CaféManagementSystem.Application_.DTOs.Employees;
using Application__CaféManagementSystem.Application_.Models;
using Core_CaféManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application__CaféManagementSystem.Application_.Interface
{
    public interface IEmployeeService
    {
        Task<ResponseModel<IEnumerable<EmployeeDto>>> GetAllEmployeeAsync();
        Task<ResponseModel<EmployeeDto>> GetEmployeeByIdAsync(int id);
        Task<ResponseModel<EmployeeDto>> AddEmployeeAsync(CreateEmployeeDto employee);
        Task<ResponseModel<EmployeeDto>> UpdateEmployeeAsync(int id, UpdateEmployeeDto employee);
        Task<ResponseModel<EmployeeDto>> DeleteEmployeeAsync(int id);
        Task<ResponseModel<bool>> ChangeIsActiveEmployeeAsync(int id, bool isActive);
        Task<ResponseModel<List<Employee>>> SearchEmployeesAsync(EmployeeSearchRequestDto employee);

    }
}

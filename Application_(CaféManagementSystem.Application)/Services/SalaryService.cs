using Application__CaféManagementSystem.Application_.DTOs.Salary;
using Application__CaféManagementSystem.Application_.Helpers;
using Application__CaféManagementSystem.Application_.Interface;
using Application__CaféManagementSystem.Application_.Models;
using Core_CaféManagementSystem.Core.Common;
using Core_CaféManagementSystem.Core.Entities;
using Core_CaféManagementSystem.Core.Interface;
using Infrastructure__CaféManagementSystem.Infrastructure_.Data.UnitofWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application__CaféManagementSystem.Application_.Services
{
    public class SalaryService : ISalaryService
    {
        private readonly IUnitofWork _unitofWork;
        public SalaryService( IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }
        public decimal GetBaseSalary(string roleName)
        {
            try
            {
                if (!Constants.RoleBaseSalary.TryGetValue(roleName, out var baseRate))
                {
                    throw new ArgumentException("Invalid role name", nameof(roleName));
                }
                return baseRate;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi GetBaseSalary" + ex.Message);
            }
        }
      
        public ResponseSalaryDto MapSalary(Salary salary)
        {
            try
            {
                if(salary.Employee == null)
                    throw new ArgumentException("Employee is null", nameof(salary.Employee));
                return new ResponseSalaryDto
                {
                    SalaryId = salary.SalaryId,
                    EmployeeId = salary.EmployeeId,
                    EmployeeName = salary.Employee.FullName,
                    RoleName = salary.Employee.Position,
                    BaseSalary = salary.BaseSalary,
                    Month = salary.Month,
                    Year = salary.Year,
                    Bonus = salary.Bonus,
                    TotalSalary = salary.TotalAmount
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi MapSalary" + ex.Message);
            }
        }
        public async Task<ResponseModel<ResponseSalaryDto>> CreateSalaryAsync(CreateSalaryDto salaryDto)
        {
            await _unitofWork.BeginTransactionAsync();
            try
            {
                if (salaryDto == null)
                {
                    return ResponseFactory.Fail<ResponseSalaryDto>("SalaryDto is null");
                }
                var employee = await _unitofWork.Employees.GetByIdAsync(salaryDto.EmployeeId);
                if (employee == null)
                {
                    return ResponseFactory.NotFound<ResponseSalaryDto>("Không tìm thấy nhân viên");
                }
                var salary = new Salary
                (
                    salaryDto.EmployeeId,
                    (byte) DateTime.Now.Month,
                    DateTime.Now.Year,
                    GetBaseSalary(employee.Position),
                    salaryDto.Bonus
                );
                await _unitofWork.Salaries.AddAsync(salary);
                await _unitofWork.SaveChangesAsync();
                await _unitofWork.CommitTransactionAsync();
                return ResponseFactory.Success(MapSalary(salary),"Tạo lương thành công");
            }
            catch (Exception ex)
            {
                await _unitofWork.RollbackTransactionAsync();
                return ResponseFactory.Error<ResponseSalaryDto>("Lỗi SalaryService", ex);
            }
        }

        public async Task<ResponseModel<UpdateSalaryDto>> UpdateSalaryAsync(UpdateSalaryDto salaryDto)
        {
            if (salaryDto == null)
            {
                return ResponseFactory.Fail<UpdateSalaryDto>("SalaryDto is null");
            }
            await _unitofWork.BeginTransactionAsync();
            try
            {
                var salary = await _unitofWork.Salaries.GetByIdAsync(salaryDto.SalaryId);
                if (salary == null)
                {
                    return ResponseFactory.NotFound<UpdateSalaryDto>("Không tìm thấy lương");
                }
                salary.UpdateSalary(salaryDto.NewBaseSalary, salaryDto.NewBonus);
                await _unitofWork.SaveChangesAsync();
                await _unitofWork.CommitTransactionAsync();
                return ResponseFactory.Success(salaryDto, "Cập nhật lương thành công");
            }
            catch (Exception ex)
            {
                await _unitofWork.RollbackTransactionAsync();
                return ResponseFactory.Error<UpdateSalaryDto>("Lỗi SalaryService", ex);
            }
        }

        public async Task<ResponseModel<IEnumerable<ResponseSalaryDto>>> GetAllSalaryAsync()
        {
            var salaries = await _unitofWork.Salaries.GetAllAsync();
            if (salaries == null)
            {
                return ResponseFactory.NotFound<IEnumerable<ResponseSalaryDto>>("Không tìm thấy lương");
            }
            var response = salaries.Select(MapSalary).ToList();
            return ResponseFactory.Success(response.AsEnumerable(), "Lấy danh sách lương thành công");
        }
    }
}

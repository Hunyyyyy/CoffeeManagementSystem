using Application__CaféManagementSystem.Application_.DTOs.Employees;
using Application__CaféManagementSystem.Application_.Helpers;
using Application__CaféManagementSystem.Application_.Interface;
using Application__CaféManagementSystem.Application_.Models;
using Application__CaféManagementSystem.Application_.Validators;
using Core_CaféManagementSystem.Core.Entities;
using Core_CaféManagementSystem.Core.Interface;
using Infrastructure__CaféManagementSystem.Infrastructure_.Data.UnitofWork;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace Application__CaféManagementSystem.Application_.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitofWork _unitofWork;
        public EmployeeService(IEmployeeRepository employeeRepository,IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }
        private EmployeeDto MapEmployeeToEmployeeDto(Employee employee)
        {
            return new EmployeeDto
            {
                EmployeeId = employee.EmployeeId,
                FullName = employee.FullName,
                Position = employee.Position,
                Email = employee.Email,
                Phone = employee.Phone,
                HireDay = employee.HireDate,
                IsActive = employee.IsActive
            };
        }
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase);
            return regex.IsMatch(email);
        }
        public async Task<ResponseModel<EmployeeDto>> AddEmployeeAsync(CreateEmployeeDto employee)
        {
            await _unitofWork.BeginTransactionAsync();
            try
            {
                if (employee == null)
                {
                    return ResponseFactory.Fail<EmployeeDto>("Employee data is required.");
                }
                if (string.IsNullOrEmpty(employee.FullName))
                {
                    return ResponseFactory.Fail<EmployeeDto>("Employee full name is required.");
                }
                if (string.IsNullOrEmpty(employee.Position))
                {
                    return ResponseFactory.Fail<EmployeeDto>("Employee position is required.");
                }
                if (string.IsNullOrEmpty(employee.Phone))
                {
                    return ResponseFactory.Fail<EmployeeDto>("Employee phone is required.");
                }
                if (employee.Phone.Length < 10 || employee.Phone.Length > 11 || employee.Phone.Any(c => !char.IsDigit(c)) || !employee.Phone.StartsWith("0"))
                {
                    return ResponseFactory.Fail<EmployeeDto>("Employee phone is invalid.");
                }
                if ((employee.Email != null) && !IsValidEmail(employee.Email))
                {
                    return ResponseFactory.Fail<EmployeeDto>("Employee email is invalid.");
                }
                if (await _unitofWork.Employees.GetByPhoneAsync(employee.Phone) != null)
                {
                    return ResponseFactory.Fail<EmployeeDto>("Employee phone is already in use.");
                }
                if (await _unitofWork.Employees.GetByEmailAsync(employee.Email ?? "") != null)
                {
                    return ResponseFactory.Fail<EmployeeDto>("Employee email is already in use.");
                }
                var newEmployee = new Employee
               (employee.FullName, employee.Position, employee.Email, employee.Phone, employee.HireDay);
                await _unitofWork.Employees.AddAsync(newEmployee);
                await _unitofWork.SaveChangesAsync();
                await _unitofWork.CommitTransactionAsync();
                return ResponseFactory.Success(MapEmployeeToEmployeeDto(newEmployee),"Thêm nhân viên thành công");
            }
            catch (Exception ex)
            {
                await _unitofWork.RollbackTransactionAsync();
                return ResponseFactory.Fail<EmployeeDto>(ex.Message);
            }

        }

        public async Task<ResponseModel<EmployeeDto>> DeleteEmployeeAsync(int id)
        {
            var employee = await _unitofWork.Employees.GetByIdAsync(id);
            if (employee == null)
            {
                return ResponseFactory.Fail<EmployeeDto>("Employee not found.");
            }
            await _unitofWork.BeginTransactionAsync();
            try
            {
                await _unitofWork.Employees.DeleteAsync(id);
                await _unitofWork.SaveChangesAsync();
                await _unitofWork.CommitTransactionAsync();
                return ResponseFactory.Success(MapEmployeeToEmployeeDto(employee), "Xóa nhân viên thành công");
            }
            catch (Exception ex)
            {
                await _unitofWork.RollbackTransactionAsync();
                return ResponseFactory.Fail<EmployeeDto>(ex.Message);
            }
        }

        public async Task<ResponseModel<IEnumerable<EmployeeDto>>> GetAllEmployeeAsync()
        {
            return ResponseFactory.Success((await _unitofWork.Employees.GetAllAsync()).Select(MapEmployeeToEmployeeDto), "Lấy danh sách nhân viên thành công");

        }

        public async Task<ResponseModel<EmployeeDto>> GetEmployeeByIdAsync(int id)
        {
            var employee = await _unitofWork.Employees.GetByIdAsync(id);
            if (employee == null)
            {
                return ResponseFactory.Fail<EmployeeDto>("Employee not found.");
            }
            return ResponseFactory.Success(MapEmployeeToEmployeeDto(employee), "Lấy thông tin nhân viên thành công");
        }

        public async Task<ResponseModel<EmployeeDto>> UpdateEmployeeAsync(int id, UpdateEmployeeDto employee)
        {
            var existingEmployee =await _unitofWork.Employees.GetByIdAsync(id);
            if (existingEmployee == null)
            {
                return ResponseFactory.Fail<EmployeeDto>("Employee not found.");
            }
            await _unitofWork.BeginTransactionAsync();
            try
            {
                if (employee == null)
                {
                    return ResponseFactory.Fail<EmployeeDto>("Employee data is required.");
                }
                if (string.IsNullOrEmpty(employee.FullName))
                {
                    return ResponseFactory.Fail<EmployeeDto>("Employee full name is required.");
                }
                if (string.IsNullOrEmpty(employee.Position))
                {
                    return ResponseFactory.Fail<EmployeeDto>("Employee position is required.");
                }
                if (string.IsNullOrEmpty(employee.Phone))
                {
                    return ResponseFactory.Fail<EmployeeDto>("Employee phone is required.");
                }
                if (employee.Phone.Length < 10 || employee.Phone.Length > 11 || employee.Phone.Any(c => !char.IsDigit(c)) || !employee.Phone.StartsWith("0"))
                {
                    return ResponseFactory.Fail<EmployeeDto>("Employee phone is invalid");
                }
                if ((employee.Email != null) && !IsValidEmail(employee.Email))
                {
                    return ResponseFactory.Fail<EmployeeDto>("Employee email is invalid.");
                }
                existingEmployee.UpdateInfo(employee.FullName, employee.Position, employee.Phone);
                await _unitofWork.SaveChangesAsync();
                await _unitofWork.CommitTransactionAsync();
                return ResponseFactory.Success(MapEmployeeToEmployeeDto(existingEmployee), "Cập nhật thông tin nhân viên thành công");
            }
            catch (Exception ex)
            {
                await _unitofWork.RollbackTransactionAsync();
                return ResponseFactory.Fail<EmployeeDto>(ex.Message);

            }
            }

        public async Task<ResponseModel<bool>> ChangeIsActiveEmployeeAsync(int id, bool isActive)
        {
            bool result = await _unitofWork.Employees.ChangeIsActiveAsync(isActive, id);
            if (result)
            {
                await _unitofWork.SaveChangesAsync();
                return ResponseFactory.Success(result, "Thay đổi trạng thái nhân viên thành công");
            }

            return ResponseFactory.Fail<bool>("Employee not found.");
        }

        public async Task<ResponseModel<List<Employee>>> SearchEmployeesAsync(EmployeeSearchRequestDto employee)
        {
            try
            {
                var query = _unitofWork.Employees.GetEmployees(); // Đảm bảo GetEmployees() không là async Task<IQueryable<T>>

                if (employee.Id.HasValue)
                    query = query.Where(e => e.EmployeeId == employee.Id.Value);
                if (!string.IsNullOrEmpty(employee.Name))
                    query = query.Where(e => EF.Functions.Like(e.FullName, $"%{employee.Name}%")); // Tìm không phân biệt chữ hoa/thường
                if (!string.IsNullOrEmpty(employee.Phone))
                    query = query.Where(e => e.Phone == employee.Phone);
                if (!string.IsNullOrEmpty(employee.Email))
                    query = query.Where(e => e.Email == employee.Email);

                var result = await query.ToListAsync(); // Thực thi truy vấn ở đây

                return ResponseFactory.Success(result, "Tìm kiếm nhân viên thành công");
            }
            catch (Exception ex)
            {
                return ResponseFactory.Fail<List<Employee>>("Không tìm thấy" + ex.Message);

            }
        }

    }
}

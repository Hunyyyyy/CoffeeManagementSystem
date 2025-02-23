using Application__CaféManagementSystem.Application_.DTOs.Employees;
using Application__CaféManagementSystem.Application_.Helpers;
using Application__CaféManagementSystem.Application_.Models;
using Infrastructure__CaféManagementSystem.Infrastructure_.Data.UnitofWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Application__CaféManagementSystem.Application_.Validators
{
    public class EmployeeValidator
    {
        private readonly IUnitofWork _unitOfWork;

        public EmployeeValidator(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase);
            return regex.IsMatch(email);
        }
        //public async Task<ResponseModel<CreateEmployeeDto>> ValidateAsync(CreateEmployeeDto employee)
        //{
        //    if (employee == null)
        //    {
        //        return ResponseFactory.Fail<CreateEmployeeDto>("Employee data is required.");
        //    }
        //    if (string.IsNullOrEmpty(employee.FullName))
        //    {
        //        return ResponseFactory.Fail<CreateEmployeeDto>("Employee full name is required.");
        //    }
        //    if (string.IsNullOrEmpty(employee.Position))
        //    {
        //        return ResponseFactory.Fail<CreateEmployeeDto>("Employee position is required.");
        //    }
        //    if (string.IsNullOrEmpty(employee.Phone))
        //    {
        //        return ResponseFactory.Fail<CreateEmployeeDto>("Employee phone is required.");
        //    }
        //    if (employee.Phone.Length < 10 || employee.Phone.Length > 11 || employee.Phone.Any(c => !char.IsDigit(c)) || !employee.Phone.StartsWith("0"))
        //    {
        //        return ResponseFactory.Fail<CreateEmployeeDto>("Employee phone is invalid.");
        //    }
        //    if((employee.Email != null) && !IsValidEmail(employee.Email))
        //    {
        //        return ResponseFactory.Fail<CreateEmployeeDto>("Employee email is invalid.");
        //    }

        //    // 🔥 Kiểm tra số điện thoại đã tồn tại chưa
        //    //var existingEmployee = await _unitOfWork.Employees.GetByPhoneAsync(employee.Phone);
        //    //if (existingEmployee != null)
        //    //{
        //    //    return ResponseFactory.Fail<CreateEmployeeDto>("Employee phone already exists.");
        //    //}

        //    //// 🔥 Kiểm tra email đã tồn tại chưa (nếu có)
        //    //if (!string.IsNullOrEmpty(employee.Email))
        //    //{
        //    //    var existingEmail = await _unitOfWork.Employees.GetByEmailAsync(employee.Email);
        //    //    if (existingEmail != null)
        //    //    {
        //    //        return ResponseFactory.Fail<CreateEmployeeDto>("Employee email already exists.");
        //    //    }
        //    //}

        //    return ResponseFactory.Success<CreateEmployeeDto>(employee,"ss");
        //}
    }
}

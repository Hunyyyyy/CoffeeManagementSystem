using Application__CaféManagementSystem.Application_.DTOs.Users;
using Application__CaféManagementSystem.Application_.Helpers;
using Application__CaféManagementSystem.Application_.Interface;
using Application__CaféManagementSystem.Application_.Models;
using Core_CaféManagementSystem.Core.Common;
using Core_CaféManagementSystem.Core.Entities;
using Infrastructure__CaféManagementSystem.Infrastructure_.Data.UnitofWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application__CaféManagementSystem.Application_.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitofWork _unitOfWork;
        public UserService(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public UserRoleDto MapRole(UserRole role)
        {
            return new UserRoleDto
            {
                RoleId = role.RoleId,
                RoleName = role.RoleName
            };
        }
        public UserResponseDto MapUser(User user, UserRole role)
        {
            return new UserResponseDto
            {
                UserId = user.UserId,
                EmployeeId = user.EmployeeId,
                Username = user.Username,
                Password = user.PasswordHash,
                LastLogin = user.LastLogin,
                Role = MapRole(role)
            };
        }
        public async Task<ResponseModel<bool>> AddNewUser(CreateUserDto user)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                if (user == null)
                    return ResponseFactory.Fail<bool>("User data is required");
                bool userEx = await _unitOfWork.Users.ExistsAsync(user.EmployeeId);
                var checkUserName =await _unitOfWork.Users.GetByUsernameAsync(user.Username);
                if (userEx || checkUserName != null) { 
                return ResponseFactory.Fail<bool>("User đã tồn tại");
                }
                // 🔹 Kiểm tra Employee tồn tại và đang hoạt động
                var employee = await _unitOfWork.Employees.GetByIdAsync(user.EmployeeId);
                if (employee == null)
                    return ResponseFactory.NotFound<bool>($"Employee with id {user.EmployeeId} not found");

                if (!employee.IsActive)
                    return ResponseFactory.Fail<bool>("Nhân viên không được Active,không thể tạo Tài khoản");
                // 🔹 Lấy RoleId từ Position của Employee
                
                int? roleId = Constants.RoleMappings.FirstOrDefault(x => x.Value == employee.Position).Key;
                if (roleId == null)
                    return ResponseFactory.Fail<bool>("Không tìm thấy RoleId cho vị trí này");

                // 🔹 Kiểm tra Role có tồn tại không
                var role = await _unitOfWork.UserRoles.GetByIdAsync(roleId.Value);
                if (role == null)
                    return ResponseFactory.NotFound<bool>($"Role với ID {roleId} không tồn tại");

                // 🔹 Tạo User mới
                var newUser = new User(user.EmployeeId, user.Username, user.PasswordHash, roleId.Value);
                await _unitOfWork.Users.AddAsync(newUser);


                // 🔹 Commit transaction
                await _unitOfWork.CommitTransactionAsync();
                await _unitOfWork.SaveChangesAsync();
                return ResponseFactory.Success(true, "Thêm thành công");
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return ResponseFactory.Fail<bool>("Thêm thất bại (Service): " + ex.Message);
            }
        }


        public Task<ResponseModel<bool>> DeleteUser(int employeeId)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel<List<UserResponseDto>>> GetAllUserAsync()
        {
            var users = await _unitOfWork.Users.GetAll().ToListAsync();

            if (!users.Any()) // Kiểm tra danh sách rỗng thay vì null
                return ResponseFactory.NotFound<List<UserResponseDto>>("Không tìm thấy user nào");

            // Lấy tất cả role trong 1 lần để tránh gọi DB nhiều lần
            var roles = await _unitOfWork.UserRoles.GetAll().ToListAsync();
            var roleDictionary = roles.ToDictionary(r => r.RoleId);

            var userResponseDtos = new List<UserResponseDto>();

            foreach (var user in users)
            {
                if (!roleDictionary.TryGetValue(user.RoleId, out var role))
                    return ResponseFactory.NotFound<List<UserResponseDto>>($"Không tìm thấy role cho user {user.Username}");

                userResponseDtos.Add(MapUser(user, role));
            }

            return ResponseFactory.Success(userResponseDtos, "Lấy danh sách user thành công");
        }


        public Task<ResponseModel<bool>> UpdateUser(UserUpdateDto user)
        {
            throw new NotImplementedException();
        }
    }
}

using Application__CaféManagementSystem.Application_.DTOs.Users;
using Application__CaféManagementSystem.Application_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application__CaféManagementSystem.Application_.Interface
{
    public interface IUserService
    {
        Task<ResponseModel<bool>> AddNewUser(CreateUserDto user);
        Task<ResponseModel<bool>> UpdateUser(UserUpdateDto user);
        Task<ResponseModel<bool>> DeleteUser(int employeeId);
        Task<ResponseModel<List<UserResponseDto>>> GetAllUserAsync();
    }
}

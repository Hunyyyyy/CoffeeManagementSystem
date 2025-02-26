using Application__CaféManagementSystem.Application_.DTOs.Users;
using Application__CaféManagementSystem.Application_.Models;
using Core_CaféManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application__CaféManagementSystem.Application_.Interface
{
    public interface IAuthService
    {
        Task<ResponseModel<string>> LoginAsync(UserDto user);
        Task<ResponseModel<string>> GetRoleNameByIdAsync(int roleId);
        Task<ResponseModel<string>?> RefreshTokenAsync();
        //Task<bool> isAdminOrManager(string roleId);
    }
}

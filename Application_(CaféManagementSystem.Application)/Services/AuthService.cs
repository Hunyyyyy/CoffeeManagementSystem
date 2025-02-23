using Application__CaféManagementSystem.Application_.DTOs.Users;
using Application__CaféManagementSystem.Application_.Helpers;
using Application__CaféManagementSystem.Application_.Interface;
using Application__CaféManagementSystem.Application_.Models;
using Core_CaféManagementSystem.Core.Entities;
using Infrastructure__CaféManagementSystem.Infrastructure_.Data.UnitofWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application__CaféManagementSystem.Application_.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitofWork _unitOfWork;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IJwtProvider _jwtProvider;
        public AuthService(IUnitofWork unitofWork,IJwtProvider jwtProvider,IPasswordHasher<User> passwordHasher)
        {
            _unitOfWork = unitofWork;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
        }
        public Task<ResponseModel<User>> AddNewAccount(CreateUserDto user)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel<string>> GetRoleNameByIdAsync(int roleId)
        {
            var roleName =  await _unitOfWork.Users.GetRoleNameByIdAsync(roleId);
            if(roleName == null)
                return ResponseFactory.NotFound<string>("Không tìm thấy quyền");
            return ResponseFactory.Success(roleName, "Lấy tên quyền thành công");
        }

        //update thêm ResponseAllTypeFactory,tạo thêm IResponseFactory
        public async Task<ResponseModel<string>> LoginAsync(UserDto user)
        {
            var userEx =await _unitOfWork.Users.GetByUsernameAsync(user.Username);
            if (userEx == null)
                return ResponseFactory.NotFound<string>("Người dùng không tồn tại");
            if(user.Password != userEx.PasswordHash)
                return ResponseFactory.Fail<string>("Mật khẩu không chính xác");

            //Chưa xử lý phần này
            //if (_passwordHasher.VerifyHashedPassword(userEx, userEx.PasswordHash, user.Password) == PasswordVerificationResult.Failed)
            //    return ResponseFactory.Fail<string>("Mật khẩu không chính xác");
            var token = await _jwtProvider.GenerateJwtToken(userEx);
            return ResponseFactory.Success(token,"Đăng nhập thành công");

        }

        public async Task<ResponseModel<string>?> RefreshTokenAsync()
        {
            var token = await _jwtProvider.ValidateAndGenerateAccessToken();

            if (token == null)
            {
                return ResponseFactory.Fail<string>("Refresh Token không hợp lệ hoặc đã hết hạn");
            }

            return ResponseFactory.Success(token, "Lấy token thành công");
        }

    }
}

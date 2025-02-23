using Application__CaféManagementSystem.Application_.DTOs.Users;
using Application__CaféManagementSystem.Application_.Helpers;
using Application__CaféManagementSystem.Application_.Interface;
using Application__CaféManagementSystem.Application_.Models;
using Core_CaféManagementSystem.Core.Entities;
using Infrastructure__CaféManagementSystem.Infrastructure_.Data.UnitofWork;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application__CaféManagementSystem.Application_.Provider
{
    public class JwtProvider : IJwtProvider
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitofWork _unitOfWork;

        public JwtProvider(IOptions<JwtSettings> jwtSettings, IHttpContextAccessor httpContextAccessor, IUnitofWork unitOfWork)
        {
            _jwtSettings = jwtSettings.Value ?? throw new ArgumentNullException(nameof(jwtSettings));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _unitOfWork = unitOfWork;
        }
        //thiếu RefreshToken
        public Task<string?> ValidateAndGenerateAccessToken()
        {
            throw new NotImplementedException();
            //var context = _httpContextAccessor.HttpContext;
            //if (context == null || !context.Request.Cookies.TryGetValue("refreshToken", out var refreshToken))
            //{
            //    return null; // Không có Refresh Token
            //}

            //try
            //{
            //    // 🔥 Kiểm tra xem Refresh Token có tồn tại trong Database không
            //    var storedRefreshToken = await _unitOfWork.RefreshTokens.GetByTokenAsync(refreshToken);
            //    if (storedRefreshToken == null || storedRefreshToken.ExpiryDate < DateTime.UtcNow)
            //    {
            //        return null; // Token không hợp lệ hoặc đã hết hạn
            //    }

            //    // 🔥 Lấy thông tin User từ Database
            //    var user = await _unitOfWork.Users.GetByIdAsync(storedRefreshToken.UserId);
            //    if (user == null) return null;

            //    // ✅ Tạo Access Token mới
            //    var newAccessToken = await GenerateJwtToken(user);

            //    return newAccessToken;
            //}
            //catch
            //{
            //    return null;
            //}
        }

        public Task<string> GenerateJwtToken(User user)
        {
            if (string.IsNullOrEmpty(_jwtSettings.Key) ||
                string.IsNullOrEmpty(_jwtSettings.Issuer) ||
                string.IsNullOrEmpty(_jwtSettings.Audience))
            {
                throw new InvalidOperationException("JWT settings are not configured properly.");
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.RoleId.ToString())
            };
            //Đây là khóa bí mật để ký token.
            //Dùng thuật toán mã hóa HmacSha256 để đảm bảo token không thể bị giả mạo.

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            //Tạo chữ ký số để xác thực token.
            //Giúp server kiểm tra token có bị thay đổi hay không.
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
           

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds);
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token).Trim();
            // 🔥 Tạo Refresh Token (7 ngày)
            var refreshToken = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            //chưa lưu rf token vào db
            // ✅ Lưu Refresh Token vào Cookie
            _httpContextAccessor.HttpContext?.Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions
            {
                HttpOnly = true,  // 🔐 Chống XSS
                Secure = true,    // 🔒 Chỉ gửi qua HTTPS
                SameSite = SameSiteMode.Strict, // 🛡 Chống CSRF
                Expires = DateTime.UtcNow.AddDays(7) // ⏳ Refresh Token hết hạn sau 7 ngày
            });

            return Task.FromResult(tokenString);

        }
        /*
            Tham số	                            Ý nghĩa
           _configuration["Jwt:Issuer"]	        Ai phát hành token (server).
           _configuration["Jwt:Audience"]	        Ai có thể sử dụng token (client).
           claims	                                Các thông tin (username, role,...) lưu trong token.
           expires: DateTime.UtcNow.AddHours(1)	Thời gian hết hạn (1 giờ).
           signingCredentials: creds	            Cách ký token để đảm bảo an toàn.
            */
    }
}

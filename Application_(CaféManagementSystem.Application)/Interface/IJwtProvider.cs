using Application__CaféManagementSystem.Application_.DTOs.Users;
using Core_CaféManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application__CaféManagementSystem.Application_.Interface
{
    public interface IJwtProvider
    {
        Task<string> GenerateJwtToken(User user);
        Task<string?> ValidateAndGenerateAccessToken();
    }
}

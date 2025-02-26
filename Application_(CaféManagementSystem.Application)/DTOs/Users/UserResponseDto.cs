using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application__CaféManagementSystem.Application_.DTOs.Users
{
    public class UserResponseDto
    {
        public int UserId { get; set; }
        public  int EmployeeId { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public UserRoleDto? Role { get; set; }
        public DateTime? LastLogin { get; set; }
    }
}

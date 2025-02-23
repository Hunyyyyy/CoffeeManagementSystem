using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application__CaféManagementSystem.Application_.DTOs.Users
{
    public class UserRoleDto
    {
        public int RoleId { get; set; }
        public required string RoleName { get; set; }
    }
}

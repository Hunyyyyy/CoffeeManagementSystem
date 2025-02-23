using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application__CaféManagementSystem.Application_.DTOs.Users
{
    public class UserDto
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}

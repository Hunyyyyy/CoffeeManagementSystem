using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_CaféManagementSystem.Core.Entities
{
    public class UserRole
    {
        [Key]
        public int RoleId { get; private set; } // Khóa chính
        public string RoleName { get; private set; } // Tên vai trò (Unique, Not Null)

        // Constructor để tạo role mới
        public UserRole(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
                throw new ArgumentException("Tên vai trò không được để trống!");

            RoleName = roleName;
        }

        // Hành vi: Cập nhật tên vai trò
        public void UpdateRoleName(string newRoleName)
        {
            if (string.IsNullOrWhiteSpace(newRoleName))
                throw new ArgumentException("Tên vai trò không được để trống!");

            RoleName = newRoleName;
        }
    }

}

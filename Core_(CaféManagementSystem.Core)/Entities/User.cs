using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_CaféManagementSystem.Core.Entities
{
    public class User
    {
        public int UserId { get; private set; } // Khóa chính
        public int EmployeeId { get; private set; } // Khóa ngoại tham chiếu đến Employees (mỗi nhân viên có 1 tài khoản)
        public string Username { get; private set; } // Tên đăng nhập (Unique, Not Null)
        public string PasswordHash { get; private set; } // Mật khẩu đã băm (Not Null)
        public int RoleId { get; private set; } // Khóa ngoại tham chiếu đến UserRoles
        public DateTime? LastLogin { get; private set; } // Thời gian đăng nhập lần cuối (có thể null)
        public  virtual Employee? Employee { get;  set; }
        public  virtual UserRole? Role { get; set; }
        // Constructor để tạo tài khoản người dùng
        public User(int employeeId, string username, string passwordHash,int roleId)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Tên đăng nhập không được để trống!");

            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new ArgumentException("Mật khẩu không hợp lệ!");

            EmployeeId = employeeId;
            Username = username;
            PasswordHash = passwordHash;
            RoleId = roleId;
        }

        // Hành vi: Cập nhật mật khẩu (sau khi đã băm)
        public void UpdatePassword(string newPasswordHash)
        {
            if (string.IsNullOrWhiteSpace(newPasswordHash))
                throw new ArgumentException("Mật khẩu không hợp lệ!");

            PasswordHash = newPasswordHash;
        }

        // Hành vi: Cập nhật vai trò người dùng
        public void UpdateRole(int newRoleId)
        {
            RoleId = newRoleId;
        }

        // Hành vi: Ghi nhận lần đăng nhập cuối
        public void UpdateLastLogin()
        {
            LastLogin = DateTime.UtcNow;
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_CaféManagementSystem.Core.Entities
{
    public class Employee
    {
        public int EmployeeId { get; private set; }  // Khóa chính
        public string FullName { get; private set; }
        public string Position { get; private set; }
        public string Email { get; private set; }
        public string? Phone { get; private set; } // Có thể null
        public DateTime HireDate { get; private set; }
        public bool IsActive { get; private set; } = true;

        // Constructor để khởi tạo nhân viên mới
        public Employee(string fullName, string position, string email, string? phone, DateTime hireDate)
        {
            FullName = fullName;
            Position = position;
            Email = email;
            Phone = phone;
            HireDate = hireDate;
            IsActive = true;  // Mặc định là đang hoạt động
        }

        // Hành vi: Cập nhật thông tin nhân viên
        public void UpdateInfo(string fullName, string position, string? phone)
        {
            FullName = fullName;
            Position = position;
            Phone = phone;
        }

        // Hành vi: Vô hiệu hóa nhân viên
        public void Deactivate()
        {
            IsActive = false;
        }

        // Hành vi: Kích hoạt lại nhân viên
        public void Activate()
        {
            IsActive = true;
        }
    }
}

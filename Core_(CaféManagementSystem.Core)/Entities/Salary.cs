using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_CaféManagementSystem.Core.Entities
{
    public class Salary
    {
        public int SalaryId { get; private set; } // Khóa chính
        public int EmployeeId { get; private set; }  // Khóa ngoại tham chiếu đến Employees
        public byte Month { get; private set; } // Tháng lương (1-12)
        public int Year { get; private set; } // Năm lương (lớn hơn 2000)
        public decimal BaseSalary { get; private set; } // Lương cơ bản
        public decimal Bonus { get; private set; } // Thưởng (mặc định là 0)
        public decimal TotalAmount { get; private set; } // Tổng lương
        public required virtual Employee Employee { get; set; }
        // Constructor để tạo bản ghi lương mới
        public Salary(int employeeId, byte month, int year, decimal baseSalary, decimal bonus = 0)
        {
            if (month < 1 || month > 12)
                throw new ArgumentException("Tháng không hợp lệ! Giá trị phải từ 1 đến 12.");

            if (year <= 2000)
                throw new ArgumentException("Năm không hợp lệ! Giá trị phải lớn hơn 2000.");

            if (baseSalary < 0)
                throw new ArgumentException("Lương cơ bản không thể nhỏ hơn 0.");

            if (bonus < 0)
                throw new ArgumentException("Thưởng không thể nhỏ hơn 0.");

            EmployeeId = employeeId;
            Month = month;
            Year = year;
            BaseSalary = baseSalary;
            Bonus = bonus;
            TotalAmount = CalculateTotalSalary();
        }

        // Hành vi: Cập nhật lương cơ bản và thưởng
        public void UpdateSalary(decimal newBaseSalary, decimal newBonus)
        {
            if (newBaseSalary < 0)
                throw new ArgumentException("Lương cơ bản không thể nhỏ hơn 0.");

            if (newBonus < 0)
                throw new ArgumentException("Thưởng không thể nhỏ hơn 0.");

            BaseSalary = newBaseSalary;
            Bonus = newBonus;
            TotalAmount = CalculateTotalSalary();
        }

        // Phương thức tính tổng lương
        private decimal CalculateTotalSalary()
        {
            return BaseSalary + Bonus;
        }
    }

}

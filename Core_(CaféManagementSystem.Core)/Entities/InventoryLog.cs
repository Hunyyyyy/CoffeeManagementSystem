using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_CaféManagementSystem.Core.Entities
{
    public class InventoryLog
    {
        [Key]
        public int LogId { get; private set; }  // Khóa chính
        public int ProductId { get; private set; } // Khóa ngoại tham chiếu đến Products
        public int Quantity { get; private set; } // Số lượng nhập kho
        public DateTime LogDate { get; private set; } // Ngày nhập kho
        public int EmployeeId { get; private set; } // Nhân viên thực hiện nhập kho
        public string? Note { get; private set; } // Ghi chú (có thể null)
        public virtual Product? Product { get; set; }
        public virtual Employee? Employee { get; set; }

        // Constructor để tạo bản ghi nhập kho
        public InventoryLog(int productId, int quantity, int employeeId, string? note = null)
        {
            if (quantity <= 0)
                throw new ArgumentException("Số lượng nhập kho phải lớn hơn 0!");

            ProductId = productId;
            Quantity = quantity;
            LogDate = DateTime.UtcNow;
            EmployeeId = employeeId;
            Note = note;
        }

        // Hành vi: Cập nhật ghi chú nhập kho
        public void UpdateNote(string? newNote)
        {
            Note = newNote;
        }
    }

}

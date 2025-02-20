using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core_CaféManagementSystem.Core.Common.Enums;

namespace Core_CaféManagementSystem.Core.Entities
{
    public class CoffeeTable
    {
        [Key]
        public int TableId { get; private set; }  // Khóa chính
        public string TableNumber { get; private set; } = "Không tìm thấy bàn (EntityTabble)"; // Số bàn (Mã bàn)
        public string? QRCode { get; private set; } // Mã QR (Có thể null)
        public CoffeeTableStatus Status { get; private set; } // Trạng thái bàn
        public int Capacity { get; private set; } // Sức chứa tối đa

        private CoffeeTable () { }
        // Constructor để khởi tạo bàn mới
        public CoffeeTable(string tableNumber, string? qrCode, CoffeeTableStatus status, int capacity)
        {
          
            if (capacity <= 0)
                throw new ArgumentException("Sức chứa phải lớn hơn 0!");

            TableNumber = tableNumber;
            QRCode = qrCode;
            Status = status;
            Capacity = capacity;
        }

        // Hành vi: Cập nhật thông tin bàn
        public void UpdateInfo(string tableNumber, string? qrCode, int capacity)
        {
            if (capacity <= 0)
                throw new ArgumentException("Sức chứa phải lớn hơn 0!");

            TableNumber = tableNumber;
            QRCode = qrCode;
            Capacity = capacity;
        }

        // Hành vi: Thay đổi trạng thái bàn
        public void ChangeStatus(CoffeeTableStatus newStatus)
        => Status = newStatus;
    }

}

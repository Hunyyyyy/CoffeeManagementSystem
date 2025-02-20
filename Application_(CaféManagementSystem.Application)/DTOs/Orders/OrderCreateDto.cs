using Application__CaféManagementSystem.Application_.DTOs.Invoices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application__CaféManagementSystem.Application_.DTOs.Orders
{
     public class OrderCreateDto
    {
        public int TableId { get; set; }  // Bàn khách order
        public int EmployeeId { get; set; } // Nhân viên phục vụ
        public InvoiceCreateDto InvoiceCreateDto { get; set; } = new();
        public List<OrderDetailCreateDto> OrderDetails { get; set; } = new(); // Chi tiết sản phẩm trong đơn
    }
}

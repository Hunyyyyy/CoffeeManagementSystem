using Application__CaféManagementSystem.Application_.DTOs.Invoices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application__CaféManagementSystem.Application_.DTOs.Orders
{
    public class OrderResposeDto
    {
        public int OrderId { get; set; }
        public string? TableNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public required string Status { get; set; }

        // Nếu tạo hóa đơn ngay, bao gồm thông tin hóa đơn
        public InvoiceResponseDto? Invoice { get; set; }

        // Danh sách các chi tiết đơn hàng
        public List<OrderDetailResponseDto> OrderDetails { get; set; } = new();
    }
}

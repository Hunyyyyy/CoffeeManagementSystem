using Application__CaféManagementSystem.Application_.DTOs.Invoices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static Core_CaféManagementSystem.Core.Common.Enums;

namespace Application__CaféManagementSystem.Application_.DTOs.Orders
{
     public class OrderCreateDto
    {
        public int TableId { get; set; }  // Bàn khách order
        public PaymentCreateDto PaymentCreateDto { get; set; } = new();// Thông tin thanh toán
        public List<OrderDetailCreateDto> OrderDetails { get; set; } = new(); // Chi tiết sản phẩm trong đơn
    }
}

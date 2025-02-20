using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application__CaféManagementSystem.Application_.DTOs.Products
{
    public class ProductResponseDto
    {
        public int ProductId { get; set; }
        public required string ProductName { get; set; }
        public string? Description { get; set; } = "Không có mô tả";
        public decimal UnitPrice { get; set; }
        public int StockQuantity { get; set; }
        public string? Category { get; set; } = "Không có danh mục";
        public bool IsActive { get; set; }
    }
}

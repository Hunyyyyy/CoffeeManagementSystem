using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application__CaféManagementSystem.Application_.DTOs.Products
{
    public class ProductSearchRequestDto
    {
        public int? ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? Category { get; set; }
    }
}

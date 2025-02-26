using Application__CaféManagementSystem.Application_.DTOs.Products;
using Core_CaféManagementSystem.Core.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static Core_CaféManagementSystem.Core.Common.Enums;

namespace Application__CaféManagementSystem.Application_.DTOs.Orders
{
    public class OrderDetailCreateDto
    {

        [Required(ErrorMessage = "Products is required.")]
        public int ProductId { get; set; }
        public required int Quantity { get; set; } = 1;
        public decimal Discount { get; set; }

    }
}

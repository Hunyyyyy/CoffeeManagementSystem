using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core_CaféManagementSystem.Core.Common.Enums;

namespace Application__CaféManagementSystem.Application_.DTOs.Invoices
{
    public class InvoiceResponseDto
    {
        public decimal TotalAmount { get; set; }
        public string? PaymentStatus { get; set; }
    }
}

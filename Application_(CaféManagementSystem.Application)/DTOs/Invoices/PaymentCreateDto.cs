using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core_CaféManagementSystem.Core.Common.Enums;

namespace Application__CaféManagementSystem.Application_.DTOs.Invoices
{
    public class PaymentCreateDto
    {
        public decimal Amount { get; set; } // Số tiền thanh toán
        public PaymentMethod PaymentMethod { get; set; } // Phương thức thanh toán

    }
}

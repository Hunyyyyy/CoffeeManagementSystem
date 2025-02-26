using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application__CaféManagementSystem.Application_.DTOs.Payments
{
    public class PaymentResponseDto
    {
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public required string PaymentMethod { get; set; }
        public string? TransactionCode { get; set; }
    }
}

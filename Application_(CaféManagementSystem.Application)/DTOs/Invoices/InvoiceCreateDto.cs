using Core_CaféManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core_CaféManagementSystem.Core.Common.Enums;

namespace Application__CaféManagementSystem.Application_.DTOs.Invoices
{
    public class InvoiceCreateDto
    {
        public List<PaymentCreateDto> Payments { get; set; } = new();
    }
}

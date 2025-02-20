using Core_CaféManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core_CaféManagementSystem.Core.Common.Enums;

namespace Application__CaféManagementSystem.Application_.Interface
{
    public interface IInvoiceService
    {
        Task<Invoice> CreateInvoiceAsync(int orderId, decimal totalAmount, PaymentStatus status);
        Task UpdateStatusInvoice(decimal totalPaymentAmount, decimal totalAmount,Invoice invoice);
    }
}

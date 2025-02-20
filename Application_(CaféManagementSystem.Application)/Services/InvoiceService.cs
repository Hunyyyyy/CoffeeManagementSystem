using Application__CaféManagementSystem.Application_.Interface;
using Core_CaféManagementSystem.Core.Entities;
using Infrastructure__CaféManagementSystem.Infrastructure_.Data.UnitofWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core_CaféManagementSystem.Core.Common.Enums;

namespace Application__CaféManagementSystem.Application_.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IUnitofWork _unitofWork;
        public InvoiceService(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }
        public async Task<Invoice> CreateInvoiceAsync(int orderId, decimal totalAmount, PaymentStatus status)
        {

            var invoice =  new Invoice(orderId, totalAmount, status);
            await _unitofWork.Invoices.AddAsync(invoice);
            return invoice;
        }

        public async Task UpdateStatusInvoice(decimal totalPaymentAmount, decimal totalAmount, Invoice invoice)
        {
            // 🔹 Cập nhật trạng thái hóa đơn
            if (totalPaymentAmount >= totalAmount)
            {
                invoice.UpdatePaymentStatus(PaymentStatus.Paid);  // Thanh toán đầy đủ
            }
            else if (totalPaymentAmount > 0)
            {
                invoice.UpdatePaymentStatus(PaymentStatus.Partial);  // Thanh toán một phần
            }
            else
            {
                invoice.UpdatePaymentStatus(PaymentStatus.Unpaid);  // Chưa thanh toán
            }

            // 🔹 Lưu thay đổi vào cơ sở dữ liệu
            await _unitofWork.Invoices.UpdateAsync(invoice);
        }

    }
}

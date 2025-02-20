using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core_CaféManagementSystem.Core.Common.Enums;

namespace Core_CaféManagementSystem.Core.Entities
{
    public class Invoice
    {
        public int InvoiceId { get; private set; }  // Khóa chính
        public int OrderId { get; private set; } // Khóa ngoại, đơn hàng chỉ có 1 hóa đơn (UNIQUE)
        public DateTime InvoiceDate { get; private set; } // Ngày tạo hóa đơn
        public decimal TotalAmount { get; private set; } // Tổng số tiền hóa đơn
        public PaymentStatus PaymentStatus { get; private set; } // Trạng thái thanh toán

        public virtual Order? Order { get; set; }

        private Invoice() { }
        // Constructor để tạo hóa đơn mới
        public Invoice(int orderId, decimal totalAmount, PaymentStatus paymentStatus)
        {
            if (totalAmount < 0)
                throw new ArgumentException("Tổng tiền hóa đơn không thể âm!");

            
            OrderId = orderId;
            InvoiceDate = DateTime.UtcNow;
            TotalAmount = totalAmount;
            PaymentStatus = paymentStatus;
        }

        // Hành vi: Cập nhật trạng thái thanh toán
        public void UpdatePaymentStatus(PaymentStatus newStatus)
            => PaymentStatus = newStatus;

        // Hành vi: Cập nhật tổng số tiền hóa đơn
        public void UpdateTotalAmount(decimal newTotalAmount)
        {
            if (newTotalAmount < 0)
                throw new ArgumentException("Tổng tiền không thể âm!");

            TotalAmount = newTotalAmount;
        }
    }

}

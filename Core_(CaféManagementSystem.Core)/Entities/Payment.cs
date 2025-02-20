using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core_CaféManagementSystem.Core.Common.Enums;

namespace Core_CaféManagementSystem.Core.Entities
{
    public class Payment
    {
        public int PaymentId { get; private set; }  // Khóa chính
        public int InvoiceId { get; private set; } // Khóa ngoại tham chiếu đến Invoice
        public decimal Amount { get; private set; } // Số tiền thanh toán
        public PaymentMethod PaymentMethod { get; private set; } // Phương thức thanh toán
        public DateTime PaymentDate { get; private set; } // Ngày thanh toán
        public string? TransactionCode { get; private set; } // Mã giao dịch (có thể null)
        public virtual Invoice? Invoice { get; set; }


        // Constructor để tạo thanh toán mới
        public Payment(int invoiceId, decimal amount, PaymentMethod paymentMethod, string? transactionCode = null)
        {
            if (amount <= 0)
                throw new ArgumentException("Số tiền thanh toán phải lớn hơn 0!");

           
            InvoiceId = invoiceId;
            Amount = amount;
            PaymentMethod = paymentMethod;
            PaymentDate = DateTime.UtcNow;
            TransactionCode = transactionCode;
        }

        // Hành vi: Cập nhật phương thức thanh toán
        public void UpdatePaymentMethod(PaymentMethod newMethod)
         => PaymentMethod = newMethod;

        // Hành vi: Cập nhật số tiền thanh toán
        public void UpdateAmount(decimal newAmount)
        {
            if (newAmount <= 0)
                throw new ArgumentException("Số tiền thanh toán phải lớn hơn 0!");

            Amount = newAmount;
        }

        // Hành vi: Cập nhật mã giao dịch
        public void UpdateTransactionCode(string? newCode)
        => TransactionCode = newCode;


    }

}

using Application__CaféManagementSystem.Application_.DTOs.Invoices;
using Application__CaféManagementSystem.Application_.DTOs.Orders;
using Application__CaféManagementSystem.Application_.DTOs.Products;
using Application__CaféManagementSystem.Application_.Interface;
using Core_CaféManagementSystem.Core.Entities;
using Core_CaféManagementSystem.Core.Exceptions;
using Infrastructure__CaféManagementSystem.Infrastructure_.Data.UnitofWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core_CaféManagementSystem.Core.Common.Enums;

namespace Application__CaféManagementSystem.Application_.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitofWork _unitOfWork;
        public PaymentService(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Payment>> CreatePaymentsAsync(List<PaymentCreateDto> paymentDto)
        {
            List<Payment> payments = new List<Payment>();
            foreach (var payment in paymentDto)
            {
                var invoice = _unitOfWork.Invoices.GetByIdAsync(payment.InvoiceId).Result;
                if (invoice == null)
                {
                    throw new BusinessException($"Hóa đơn ID {payment.InvoiceId} không tồn tại!");
                }
                string randomString = ""; 
                if (!payment.PaymentMethod.Equals(PaymentMethod.Cash))
                {
                    Random random = new Random();
                    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"; // Tập ký tự bao gồm chữ cái và số
                    randomString = new string(Enumerable.Repeat(chars, 8) // 8 là độ dài chuỗi ngẫu nhiên
                        .Select(s => s[random.Next(s.Length)])
                        .ToArray());
                }
                var transactionCode = payment.PaymentMethod.Equals(PaymentMethod.Cash) ? null : randomString;
                var paymentEntity = new Payment(invoice.InvoiceId, payment.Amount, payment.PaymentMethod, transactionCode);
                await _unitOfWork.Payments.AddAsync(paymentEntity);
                payments.Add(paymentEntity);
            }
            return payments;
        }

        public async Task<Payment> PaymentMethodProcessing(PaymentCreateDto paymentDto, Invoice invoice)
        {
            // Kiểm tra phương thức thanh toán hợp lệ (Chỉ chấp nhận Cash và Bank Transfer)
            if (paymentDto.PaymentMethod != PaymentMethod.Cash && paymentDto.PaymentMethod != PaymentMethod.BankTransfer)
            {
                throw new BusinessException("Phương thức thanh toán không hợp lệ!");
            }

            // Kiểm tra xem tổng số tiền thanh toán có khớp với tổng hóa đơn không
            if (paymentDto.Amount <= 0 || paymentDto.Amount > invoice.TotalAmount)
            {
                throw new BusinessException("Số tiền thanh toán không hợp lệ!");
            }

            // Cập nhật trạng thái thanh toán của hóa đơn
            if (paymentDto.Amount == invoice.TotalAmount)
            {
                invoice.UpdatePaymentStatus(PaymentStatus.Paid);  // Thanh toán đầy đủ
            }
            else
            {
                invoice.UpdatePaymentStatus(PaymentStatus.Partial);  // Thanh toán một phần
            }

            // Thêm thông tin thanh toán vào cơ sở dữ liệu
            var payment = new Payment(invoice.InvoiceId, paymentDto.Amount, paymentDto.PaymentMethod, "PaymentService");
                 
            

            await _unitOfWork.Payments.AddAsync(payment);  // Thêm giao dịch thanh toán vào DB

            // Cập nhật hóa đơn với trạng thái thanh toán mới
            await _unitOfWork.Invoices.UpdateAsync(invoice);

            // Trả về hóa đơn đã được cập nhật
            return payment;
        }



        public Task<IEnumerable<Invoice>> ProcessInvoiceAsync(int orderId, decimal totalAmount, List<GetProductClientDto> orderDetails)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Payment>> IPaymentService.ProcessInvoiceAsync(int orderId, decimal totalAmount, List<GetProductClientDto> orderDetails)
        {
            throw new NotImplementedException();
        }
    }
}

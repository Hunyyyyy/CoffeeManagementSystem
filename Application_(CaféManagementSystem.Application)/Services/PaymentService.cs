using Application__CaféManagementSystem.Application_.DTOs.Invoices;
using Application__CaféManagementSystem.Application_.DTOs.Orders;
using Application__CaféManagementSystem.Application_.DTOs.Products;
using Application__CaféManagementSystem.Application_.Helpers;
using Application__CaféManagementSystem.Application_.Interface;
using Application__CaféManagementSystem.Application_.Models;
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
        public PaymentService(IUnitofWork unitOfWork, IInvoiceService invoiceService, ITableService tableService)
        {
            _unitOfWork = unitOfWork;

        }
        public async Task<Payment> CreatePaymentsAsync(PaymentCreateDto paymentDto,int InvoiceId)
        {
                var invoice = _unitOfWork.Invoices.GetByIdAsync(InvoiceId).Result;
                if (invoice == null)
                {
                    throw new BusinessException($"Hóa đơn ID {InvoiceId} không tồn tại!");
                }
                string randomString = ""; 
                if (!paymentDto.PaymentMethod.Equals(PaymentMethod.Cash))
                {
                    Random random = new Random();
                    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"; // Tập ký tự bao gồm chữ cái và số
                    randomString = new string(Enumerable.Repeat(chars, 8) // 8 là độ dài chuỗi ngẫu nhiên
                        .Select(s => s[random.Next(s.Length)])
                        .ToArray());
                }
                var transactionCode = paymentDto.PaymentMethod.Equals(PaymentMethod.Cash) ? null : randomString;
                var paymentEntity = new Payment(invoice.InvoiceId, paymentDto.Amount, paymentDto.PaymentMethod, transactionCode);
                await _unitOfWork.Payments.AddAsync(paymentEntity);
            
            return paymentEntity;
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

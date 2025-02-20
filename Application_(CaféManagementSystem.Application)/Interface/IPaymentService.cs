﻿using Application__CaféManagementSystem.Application_.DTOs.Invoices;
using Application__CaféManagementSystem.Application_.DTOs.Orders;
using Application__CaféManagementSystem.Application_.DTOs.Products;
using Core_CaféManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application__CaféManagementSystem.Application_.Interface
{
    public interface IPaymentService
    {
        Task<IEnumerable<Payment>> ProcessInvoiceAsync(int orderId, decimal totalAmount, List<GetProductClientDto> orderDetails);
        Task <Payment> PaymentMethodProcessing(PaymentCreateDto paymentDto, Invoice invoice);
        Task<List<Payment>> CreatePaymentsAsync(List<PaymentCreateDto> paymentDto);
    }
}

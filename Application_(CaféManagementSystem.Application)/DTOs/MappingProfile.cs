using Application__CaféManagementSystem.Application_.DTOs.Invoices;
using Application__CaféManagementSystem.Application_.DTOs.Orders;
using AutoMapper;
using Core_CaféManagementSystem.Core.Entities;
using Core_CaféManagementSystem.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core_CaféManagementSystem.Core.Common.Enums;

namespace Application__CaféManagementSystem.Application_.DTOs
{
    public class MappingProfile : Profile
    {
        //public MappingProfile() {
        //    CreateMap<OrderCreateDto, Order>()
        //        .ConstructUsing((src, dest) => new Order(
        //            src.TableId,
        //            src.EmployeeId,
        //            0, // Giá trị tạm thời cho totalAmount
        //            OrderStatus.Pending // Trạng thái mặc định
        //            ));

        //    CreateMap<OrderDetailCreateDto, OrderDetail>()
        //        .ConstructUsing((src,dest)=>new OrderDetail(
        //            src.OrderId,
        //            0,// Giá trị tạm thời cho ProductId
        //            src.Quantity,
        //            0,// Giá trị tạm thời cho UnitPrice
        //            src.Discount
        //            ));
        //    CreateMap<InvoiceCreateDto, Invoice>()
        //        .ConstructUsing((src, dest) => new Invoice(
        //            src.OrderId,
        //            0, // Giá trị tạm thời cho totalAmount
        //            PaymentStatus.Unpaid // Trạng thái mặc định
        //            )); 
        //    CreateMap<PaymentCreateDto, Payment>()
        //        .ConstructUsing((src, dest) => new Payment(
        //            src.InvoiceID,
        //            src.Amount,
        //            src.PaymentMethod,
        //            null // Chưa có TransactionCode
        //            ));

        //}
    }
}

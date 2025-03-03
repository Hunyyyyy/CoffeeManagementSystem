using Application__CaféManagementSystem.Application_.DTOs.Invoices;
using Application__CaféManagementSystem.Application_.DTOs.Orders;
using Application__CaféManagementSystem.Application_.DTOs.Products;
using Application__CaféManagementSystem.Application_.Models;
using Core_CaféManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core_CaféManagementSystem.Core.Common.Enums;

namespace Application__CaféManagementSystem.Application_.Interface
{
    public interface IOrderService
    {
        Task<ResponseModel<IEnumerable<OrderResposeDto>>> GetAllOrdersAsync();
        Task<ResponseModel<OrderResposeDto>> GetOrderByIdAsync(int id);
        Task<ResponseModel<IEnumerable<OrderResposeDto>>> GetStatusOrdersAsync(OrderStatus status);
        Task<ResponseModel<OrderResposeDto>> SendOrderFromCustomerToEmployee(
            OrderCreateDto orderCreateDto);
        Task<ResponseModel<OrderResposeDto>> ConfirmOrderAsync(EmployeeConfirmOrderDto orderDto);
        Task<Order> UpdateOrderAsync(Order order);
        Task<ResponseModel<bool>> CancelOrderAsync(int id);
    }
}

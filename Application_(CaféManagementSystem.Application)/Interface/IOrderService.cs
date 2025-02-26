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

namespace Application__CaféManagementSystem.Application_.Interface
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order> GetOrderByIdAsync(int id);
        Task<IEnumerable<Order>> GetStatusOrdersAsync();
        Task<ResponseModel<OrderResposeDto>> SendOrderFromCustomerToEmployee(
            OrderCreateDto orderCreateDto);
        Task<ResponseModel<OrderResposeDto>> ConfirmOrderAsync(EmployeeConfirmOrderDto orderDto);
        Task<Order> UpdateOrderAsync(Order order);
        Task<Order> CancelOrderAsync(int id);
    }
}

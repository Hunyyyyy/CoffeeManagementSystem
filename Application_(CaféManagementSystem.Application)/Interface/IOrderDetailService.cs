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
    public interface IOrderDetailService
    {
        Task<decimal>CalculateTotalAmount(List<OrderDetailCreateDto> orderDetails);
        Task<List<OrderDetail>> CreateOrderDetails(List<OrderDetailCreateDto> orderDetails, int orderId);
    }
}

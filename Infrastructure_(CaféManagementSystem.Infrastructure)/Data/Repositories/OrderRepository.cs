using Application__CaféManagementSystem.Application_.DTOs.Orders;
using Core_CaféManagementSystem.Core.Entities;
using Core_CaféManagementSystem.Core.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core_CaféManagementSystem.Core.Common.Enums;

namespace Infrastructure__CaféManagementSystem.Infrastructure_.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;
        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Order> AddAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            return order;
        }

        public async Task<OrderDetail> AddOrderDetailAsync(OrderDetail orderDetail)
        {
            await _context.OrderDetails.AddAsync(orderDetail);
            return orderDetail;
        }

        public Task<Order> CancelOrderAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            var order =await GetByIdAsync(id);
            if (order == null)
            {
                return false;
            }
            return true;
        }

        public Task<IEnumerable<Order>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            return await _context.Orders.Include(x => x.OrderDetails).FirstOrDefaultAsync(x => x.OrderId == id); ;
        }

        public async Task<IEnumerable<Order>> GetStatusOrdersAsync()
        {
            return await _context.Orders.Include(x => x.OrderDetails).Where(x => x.Status == OrderStatus.Pending).ToListAsync();
        }

        public Task UpdateAsync(Order entity)
        {
            _context.Orders.Update(entity);
            // Trả về sản phẩm đã được cập nhật
            return Task.CompletedTask;
        }
    }
}

using Core__CaféManagementSystem.Core_.Interface;
using Core_CaféManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_CaféManagementSystem.Core.Interface
{
    public interface IOrderRepository : RepositoriesBase<Order>
    {
        IQueryable<Order> GetStatusOrders();
        Task<OrderDetail> AddOrderDetailAsync(OrderDetail orderDetail);
        Task<Order> CancelOrderAsync(int id);
        IQueryable<OrderDetail> GetCompletedOrderDetails();
        IQueryable<Order> GetCompletedOrders();
    }
}

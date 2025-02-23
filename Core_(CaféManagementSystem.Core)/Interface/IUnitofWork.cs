using Core_CaféManagementSystem.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure__CaféManagementSystem.Infrastructure_.Data.UnitofWork
{
    public interface IUnitofWork
    {
        IOrderRepository Orders { get; }
        
        IInvoiceRepository Invoices { get; }
        IPaymentRepository Payments { get; }
        ICoffeeTableRepository CoffeeTables { get; }
        IEmployeeRepository Employees { get; }
        IInventoryRepository Inventory { get; }
        IProductRepository Products { get; }

        IUserRepository Users { get; }
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync(); // ✅ Bắt đầu giao dịch
        Task CommitTransactionAsync(); // ✅ Hoàn tất giao dịch
        Task RollbackTransactionAsync();
    }
}

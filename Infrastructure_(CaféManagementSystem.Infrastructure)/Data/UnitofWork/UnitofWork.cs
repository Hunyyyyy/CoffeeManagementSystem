using Core_CaféManagementSystem.Core.Interface;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure__CaféManagementSystem.Infrastructure_.Data.UnitofWork
{
    public class UnitofWork : IUnitofWork
    {
        private readonly AppDbContext _context;
        private IDbContextTransaction? _currentTransaction;
        public UnitofWork(
        AppDbContext context,
        IOrderRepository orderRepository,
        IInvoiceRepository invoiceRepository,
        IPaymentRepository paymentRepository,
        ICoffeeTableRepository coffeeTableRepository,
        IEmployeeRepository employeeRepository,
        IInventoryRepository inventoryRepository,
        IProductRepository productRepository)
        {
            _context = context;
            Orders = orderRepository;
            Invoices = invoiceRepository;
            Payments = paymentRepository;
            CoffeeTables = coffeeTableRepository;
            Employees = employeeRepository;
            Inventory = inventoryRepository;
            Products = productRepository;
        }
        public IOrderRepository Orders { get; }
        public IInvoiceRepository Invoices { get; }
        public IPaymentRepository Payments { get; }
        public ICoffeeTableRepository CoffeeTables { get; }
        public IEmployeeRepository Employees { get; }
        public IInventoryRepository Inventory { get; }
        public IProductRepository Products { get; }

        public async Task<int> SaveChangesAsync()
        => await _context.SaveChangesAsync();

        public async Task BeginTransactionAsync()
        {
            if (_currentTransaction != null)
                return; // Nếu đã có transaction thì không mở mới

            _currentTransaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_currentTransaction == null)
                throw new InvalidOperationException("Không có transaction nào đang chạy.");

            await _currentTransaction.CommitAsync();
            await _currentTransaction.DisposeAsync();
            _currentTransaction = null;
        }

        public async Task RollbackTransactionAsync()
        {
            if (_currentTransaction == null)
                throw new InvalidOperationException("Không có transaction nào để rollback.");

            await _currentTransaction.RollbackAsync();
            await _currentTransaction.DisposeAsync();
            _currentTransaction = null;
        }

        public void Dispose()
        {
            _currentTransaction?.Dispose();
            _context.Dispose();
        }
    }
}

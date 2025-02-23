using Core_CaféManagementSystem.Core.Entities;
using Core_CaféManagementSystem.Core.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure__CaféManagementSystem.Infrastructure_.Data.Repositories
{
    
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly AppDbContext _context;
        public InvoiceRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Invoice> AddAsync(Invoice entity)
        {
            await _context.Invoices.AddAsync(entity);
            return entity;
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            var invoice =await GetByIdAsync(id);
            if (invoice == null)
            {
                return false;
            }
            return true;
        }

        public Task<IEnumerable<Invoice>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Invoice?> GetByIdAsync(int id)
        {
            var invoice =await _context.Invoices.FindAsync(id);
            if (invoice == null)
            {
                throw new Exception($"Invoice with ID {id} not found.");
            }
            return invoice;
        }

        public async Task<Invoice> GetInvoiceByOrderIdAsync(int id)
        {
            var invoice =await _context.Invoices.FirstOrDefaultAsync(x => x.OrderId == id);
            if (invoice == null)
            {
                throw new Exception($"Invoice with Order ID {id} not found.");
            }
            return invoice;
        }

        public Task UpdateAsync(Invoice entity)
        {
            _context.Invoices.Update(entity);
            return Task.CompletedTask;
        }
    }
}

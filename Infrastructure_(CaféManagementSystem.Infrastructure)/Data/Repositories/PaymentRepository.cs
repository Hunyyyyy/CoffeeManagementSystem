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
    public class PaymentRepository : IPaymentRepository
    {
        private readonly AppDbContext _context;
        public PaymentRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Payment> AddAsync(Payment entity)
        {
            await _context.Payments.AddAsync(entity);
            return entity;

        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            var payment =await _context.Payments.FindAsync(id);
            if (payment == null)
                return false;

            return true;
        }

        public Task<IEnumerable<Payment>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Payment> GetByIdAsync(int id)
        {
            var payment =await _context.Payments.FindAsync(id);
            if (payment == null)
                throw new Exception($"Payment with ID {id} not found.");
            return payment;
        }

        public async Task<bool> HasPaymentsByInvoiceIdAsync(int invoiceId)
        {
            var payment =await _context.Payments.FirstOrDefaultAsync(x => x.InvoiceId == invoiceId);
            if (payment == null)
                return false;
            return true;
        }

        public Task UpdateAsync(Payment entity)
        {
             _context.Payments.Update(entity);
            return Task.CompletedTask;
        }
    }
}

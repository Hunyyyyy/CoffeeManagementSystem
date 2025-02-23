using Core_CaféManagementSystem.Core.Entities;
using Core_CaféManagementSystem.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure__CaféManagementSystem.Infrastructure_.Data.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly AppDbContext _context;
        public InventoryRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Inventory> AddAsync(Inventory entity)
        {
            await _context.Inventory.AddAsync(entity);
            return entity;
        }

        public Task<Inventory> AddInventoryAsync(Inventory inventory)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Inventory>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Inventory?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetByProductIdAsync(int productID)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Inventory entity)
        {
            _context.Inventory.Update(entity);
            return Task.CompletedTask;
        }
    }
}

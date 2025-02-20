using Core_CaféManagementSystem.Core.Entities;
using Core_CaféManagementSystem.Core.Interface;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure__CaféManagementSystem.Infrastructure_.Data.Repositories
{
   
    public class CoffeeTableRepository : ICoffeeTableRepository
    {
        private readonly AppDbContext _context;
        public CoffeeTableRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CoffeeTable> AddAsync(CoffeeTable entity)
        {
            await _context.CoffeeTables.AddAsync(entity);
            return entity;

        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ExistsAsync(int id)
           => await _context.CoffeeTables.AnyAsync(x => x.TableId == id);
        

        public Task<IEnumerable<CoffeeTable>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CoffeeTable>> GetAvailableTablesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<CoffeeTable> GetByIdAsync(int id)
        {
            var coffeeTable = await _context.CoffeeTables.FindAsync(id);
            if (coffeeTable == null)
            {
                throw new Exception($"Bàn với ID {id} không tồn tại.");
            }
            return coffeeTable;
        }

        public Task UpdateAsync(CoffeeTable entity)
        {
            var existingCoffeeTable = GetByIdAsync(entity.TableId).Result;
            _context.CoffeeTables.Update(existingCoffeeTable);
            return Task.CompletedTask;

        }
    }
}

using Application__CaféManagementSystem.Application_.DTOs.Salary;
using Core_CaféManagementSystem.Core.Common;
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
    public class SalaryRepository : ISalaryRepository
    {
        private readonly AppDbContext _context;
        public SalaryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Salary> AddAsync(Salary entity)
        {
            var salary = await _context.Salaries.AddAsync(entity);
            return entity;
        }

        public async Task<IEnumerable<Salary>> GetAllAsync()
        {
            return await _context.Salaries.Include(s => s.Employee).ToListAsync();
        }

        public async Task<Salary?> GetByIdAsync(int id)
        {
           return await _context.Salaries.FindAsync(id);
        }

    }
}

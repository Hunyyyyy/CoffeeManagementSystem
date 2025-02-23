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
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;
        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Employee> AddAsync(Employee entity)
        {
            await _context.Employees.AddAsync(entity);
            return entity;
        }

        public async Task<bool> ChangeIsActiveAsync(bool isActive,int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return false;
            }
            if (isActive)
            {
                employee.Activate();
            }
            else
            {
                employee.Deactivate();
            }
            return true;

        }

        public async Task<bool> DeleteAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return false;
            }
            _context.Employees.Remove(employee);
           return true;
        }

        public async Task<bool> ExistsAsync(int id)
        => await _context.Employees.AnyAsync(e => e.EmployeeId == id);

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<Employee?> GetByEmailAsync(string email)
        {
            return await _context.Employees.FirstOrDefaultAsync(e => e.Email == email);
        }

        public  async Task<Employee?> GetByIdAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                throw new Exception($"Employee with ID {id} not found.");
            }
            return employee;
        }

        public async Task<Employee?> GetByNameAync(string fullName)
        {
            return await _context.Employees.FirstOrDefaultAsync(e => e.FullName == fullName);
        }

        public async Task<Employee?> GetByPhoneAsync(string phone)
        {
            return await _context.Employees.FirstOrDefaultAsync(e => e.Phone == phone);
        }

        public IQueryable<Employee>GetEmployees()
        {
            return  _context.Employees.AsQueryable();
        }

        public Task UpdateAsync(Employee entity)
        {
            _context.Employees.Update(entity);
            return Task.CompletedTask;
        }
    }
}

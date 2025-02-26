using Core__CaféManagementSystem.Core_.Interface;
using Core_CaféManagementSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure__CaféManagementSystem.Infrastructure_.Data.Repositories
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly AppDbContext _context;
        public UserRoleRepository(AppDbContext context)
        {
            _context = context;
        }
        public Task<UserRole> AddAsync(UserRole entity)
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

        public IQueryable<UserRole> GetAll()
        {
            return _context.UserRoles.AsQueryable();
        }

        public async Task<UserRole?> GetByIdAsync(int id)
        {
            return await _context.FindAsync<UserRole>(id);
        }

        public Task UpdateAsync(UserRole entity)
        {
            throw new NotImplementedException();
        }
    }
}

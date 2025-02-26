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
    public class UserRepository : IUserRepository
    {
        private AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<User> AddAsync(User entity)
        {
            var user = await _context.Users.AddAsync(entity);
            return entity;
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Users.AnyAsync(u => u.UserId == id);
        }

        public IQueryable<User> GetAll()
        {
            return _context.Users.AsQueryable();
        }


        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);   
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.Users.Where(u=>u.Username== username).FirstOrDefaultAsync();
        }

        public async Task<string?> GetRoleNameByIdAsync(int roleId)
        {
            return await _context.UserRoles.Where(r => r.RoleId == roleId).Select(r => r.RoleName).FirstOrDefaultAsync();
        }

        public Task UpdateAsync(User entity)
        {
            throw new NotImplementedException();
        }
    }
}

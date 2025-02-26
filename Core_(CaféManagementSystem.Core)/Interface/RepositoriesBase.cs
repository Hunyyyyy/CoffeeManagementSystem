using Core_CaféManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core__CaféManagementSystem.Core_.Interface
{
    public interface RepositoriesBase<T> where T : class
    {
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task<bool> DeleteAsync(int id);
        Task<T?> GetByIdAsync(int id);
        IQueryable<T> GetAll();
        Task<bool> ExistsAsync(int id);
    }
}

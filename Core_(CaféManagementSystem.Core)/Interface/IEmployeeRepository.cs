using Core__CaféManagementSystem.Core_.Interface;
using Core_CaféManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_CaféManagementSystem.Core.Interface
{
    public interface  IEmployeeRepository : RepositoriesBase<Employee>
    {
        Task<Employee?> GetByPhoneAsync(string phone);
        Task<Employee?> GetByEmailAsync(string email);
        Task<Employee?> GetByNameAync(string fullName);
        Task<bool> ChangeIsActiveAsync(bool isActive,int id);
        IQueryable<Employee> GetEmployees();
    }
}

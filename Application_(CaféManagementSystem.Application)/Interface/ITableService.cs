using Core_CaféManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core_CaféManagementSystem.Core.Common.Enums;

namespace Application__CaféManagementSystem.Application_.Interface
{
    public interface ITableService
    {
        Task UpdateTableStatusAsync(int tableId, CoffeeTableStatus tableStatus);
        Task <CoffeeTable?> GetCoffeeTableByIdAsync(int tableId);
        Task<string> GetTableNumberByIdAsync(int tableId);


    }
}

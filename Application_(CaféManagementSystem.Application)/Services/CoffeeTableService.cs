using Application__CaféManagementSystem.Application_.Interface;
using Core_CaféManagementSystem.Core.Common;
using Core_CaféManagementSystem.Core.Entities;
using Core_CaféManagementSystem.Core.Exceptions;
using Infrastructure__CaféManagementSystem.Infrastructure_.Data.UnitofWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core_CaféManagementSystem.Core.Common.Enums;

namespace Application__CaféManagementSystem.Application_.Services
{
    public class CoffeeTableService : ITableService
    {
        private readonly IUnitofWork _unitOfWork;
        public CoffeeTableService(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<CoffeeTable?> GetCoffeeTableByIdAsync(int tableId)
        {
           var coffeeTable = _unitOfWork.CoffeeTables.GetByIdAsync(tableId);
            if (coffeeTable == null)
                throw new BusinessException("Bàn không tồn tại!");
            return coffeeTable;
        }

        public async Task<string> GetTableNumberByIdAsync(int tableId)
        {
            var coffeeTable = await _unitOfWork.CoffeeTables.GetByIdAsync(tableId);
            if (coffeeTable == null)
                throw new BusinessException("Bàn không tồn tại!");
            return coffeeTable.TableNumber;
        }

        public async Task UpdateTableStatusAsync(int tableId, CoffeeTableStatus tableStatus)
        {
            // 🔹 cập nhật trạng thái bàn
            var coffeeTable = await _unitOfWork.CoffeeTables.GetByIdAsync(tableId);
            if (coffeeTable == null)
                throw new BusinessException("Bàn không tồn tại!");

            coffeeTable.ChangeStatus(tableStatus);
            await _unitOfWork.CoffeeTables.UpdateAsync(coffeeTable);
        }
    }
}

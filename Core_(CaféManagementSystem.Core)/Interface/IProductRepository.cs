using Core__CaféManagementSystem.Core_.Interface;
using Core_CaféManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_CaféManagementSystem.Core.Interface
{
    public interface IProductRepository : RepositoriesBase<Product>
    {
        Task<IEnumerable<Product?>> GetActiveProductsAsync();
        Task<bool> ChangeActiveProduct(int id,bool status);
        Task<Product?> GetProductByName(string name);
        Task<bool> ChangeQuantityProduct(int id, int quantity);
        //Task <Product> UpdateStockQuantity(int id);

    }
}

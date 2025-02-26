using Core_CaféManagementSystem.Core.Entities;
using Core_CaféManagementSystem.Core.Exceptions;
using Core_CaféManagementSystem.Core.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure__CaféManagementSystem.Infrastructure_.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Product> AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            return product;
        }

        public async Task<bool> ChangeActiveProduct(int id, bool status)
        {
            var product =await GetByIdAsync(id);
            if (product == null)
            {
                return false;
            }
            if (status)
            {
              product.Activate();
                return true;
            }
            else
            {
                product.Deactivate();
                return true;
            }
        }

        public async Task<bool> ChangeQuantityProduct(int id, int quantity)
        {
            var product = await GetByIdAsync(id);
            if (product == null) 
                return false;
            product.AddStock(quantity);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null) return false; // Không tìm thấy sản phẩm

            product.Deactivate();
            _context.Products.Update(product); // Cập nhật lại trạng thái

            return true; // Trả về thành công
        }


        public async Task<bool> ExistsAsync(int id)
        {
            var product =await GetByIdAsync(id);
            if (product == null)
            {
                return false;
            }
            return true;
        }

        public async Task<IEnumerable<Product?>> GetActiveProductsAsync()
        {
            return await _context.Products.Where(p => p.IsActive == true).ToListAsync(); ;
        }

        public IQueryable<Product> GetAll()
        {
            return _context.Products.AsQueryable();
        }


        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public IQueryable<Product> GetProduct()
        {
            return _context.Products.AsQueryable();
        }

        public async Task<Product?> GetProductByName(string name)
        => await _context.Products.Where(p => p.ProductName == name).FirstOrDefaultAsync();


        public Task UpdateAsync(Product entity)
        {
             _context.Products.Update(entity);
            // Trả về sản phẩm đã được cập nhật
            return Task.CompletedTask;
        }

        public Task UpdateRangeAsync(IEnumerable<Product> products)
        {
            _context.Products.UpdateRange(products);
            return Task.CompletedTask;
        }
    }
}

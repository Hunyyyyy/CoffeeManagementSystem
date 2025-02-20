using Application__CaféManagementSystem.Application_.DTOs.Orders;
using Application__CaféManagementSystem.Application_.DTOs.Products;
using Application__CaféManagementSystem.Application_.Models;
using Core_CaféManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application__CaféManagementSystem.Application_.Interface
{
    public interface IProductService
    {
        Task <ResponseModel<ProductResponseDto>> GetProductByNameAsync(string name);
        Task<ResponseModel<ProductResponseDto>> AddProductAsync(CreateProductDto product);
        Task UpdateInventory(List<OrderDetail> orderDetails);
        Task<ResponseModel<IEnumerable<ProductResponseDto>>> GetAllProductsAsync();
        Task<ResponseModel<ProductResponseDto>> GetProductByIdAsync(int id);
        Task<ResponseModel<ProductResponseDto>> GetProductByIdForServiceAsync(int id);
        // Task<Product> CreateProductAsync(ProductCreateDto product);
        Task<ResponseModel<ProductResponseDto>> UpdateProductAsync(UpdateProductDto product);
        Task<ResponseModel<ProductResponseDto>> DeleteProductAsync(int id);
       // Task<bool> CheckStockQuantity(List<Product> products);
    }
}

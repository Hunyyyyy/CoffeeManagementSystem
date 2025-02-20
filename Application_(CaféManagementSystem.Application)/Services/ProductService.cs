using Application__CaféManagementSystem.Application_.DTOs.Orders;
using Application__CaféManagementSystem.Application_.DTOs.Products;
using Application__CaféManagementSystem.Application_.Helpers;
using Application__CaféManagementSystem.Application_.Interface;
using Application__CaféManagementSystem.Application_.Models;
using Core_CaféManagementSystem.Core.Entities;
using Core_CaféManagementSystem.Core.Exceptions;
using Infrastructure__CaféManagementSystem.Infrastructure_.Data.UnitofWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application__CaféManagementSystem.Application_.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitofWork _unitOfWork;

        public ProductService(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public ProductResponseDto MapProductResponseDto(Product product)
        {
            var productDto = new ProductResponseDto
            {
                ProductName = product.ProductName,
                Description = product.Description,
                UnitPrice = product.UnitPrice,
                StockQuantity = product.StockQuantity,
                Category = product.Category,
                IsActive = product.IsActive
            };
            return productDto;
        }
        public async Task<ResponseModel<ProductResponseDto>> AddProductAsync(CreateProductDto product)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var pro = await _unitOfWork.Products.GetProductByName(product.ProductName);
                if (pro != null)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return new ResponseModel<ProductResponseDto>
                    {
                        Message = "Sản phẩm đã tồn tại",
                        Success = false
                    };
                }
                var newProduct = new Product(product.ProductName, product.Description, product.UnitPrice, product.StockQuantity, product.Category);
                await _unitOfWork.Products.AddAsync(newProduct);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();
                return new ResponseModel<ProductResponseDto>
                {
                    Message = "Thêm sản phẩm thành công",
                    Success = true,
                    Data = new ProductResponseDto
                    {
                        ProductId = newProduct.ProductId,
                        ProductName = newProduct.ProductName,
                        Description = newProduct.Description,
                        UnitPrice = newProduct.UnitPrice,
                        StockQuantity = newProduct.StockQuantity,
                        Category = newProduct.Category,
                        IsActive = newProduct.IsActive
                    }
                };
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return new ResponseModel<ProductResponseDto>
                {
                    Message = ex.Message,
                    Success = false
                };
            }   
        }

        public async Task<ResponseModel<ProductResponseDto>> DeleteProductAsync(int id)
        {
            await _unitOfWork.Products.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            return new ResponseModel<ProductResponseDto>
            {
                Message = "Sản phẩm đã được hủy bỏ thành công",
                Success = true,
                Data = new ProductResponseDto
                {
                    ProductName = product.ProductName,
                    Description = product.Description,
                    UnitPrice = product.UnitPrice,
                    StockQuantity = product.StockQuantity,
                    Category = product.Category,
                    IsActive = product.IsActive
                }
            };
        }

        public async Task<ResponseModel<IEnumerable<ProductResponseDto>>> GetAllProductsAsync()
        {
            var allProducts = await _unitOfWork.Products.GetAllAsync();

            if (!allProducts.Any()) // Kiểm tra danh sách rỗng thay vì null
            {
                return new ResponseModel<IEnumerable<ProductResponseDto>>
                {
                    Success = false,
                    Message = "Không tìm thấy danh sách sản phẩm",
                    Data = Enumerable.Empty<ProductResponseDto>()
                };
            }

            var productDtos = allProducts.Select(product => new ProductResponseDto
            {
                ProductName = product.ProductName,
                Description = product.Description,
                UnitPrice = product.UnitPrice,
                StockQuantity = product.StockQuantity,
                Category = product.Category,
                IsActive = product.IsActive
            }).ToList();

            return new ResponseModel<IEnumerable<ProductResponseDto>>
            {
                Success = true,
                Message = "Lấy danh sách sản phẩm thành công",
                Data = productDtos
            };
        }


        public async Task<ResponseModel<ProductResponseDto>> GetProductByIdAsync(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product == null)
                return ResponseFactory.NotFound<ProductResponseDto>($"Sản phẩm ID {id} không tồn tại!");
            return ResponseFactory.Success(MapProductResponseDto(product), "Tìm thấy sản phẩm");
        }


        public async Task<ResponseModel<ProductResponseDto>> GetProductByIdForServiceAsync(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product == null)
                throw new BusinessException($"Sản phẩm ID {id} không tồn tại!");
            return product;
            //đang sửa Response Helpper
        }

        public async Task<ResponseModel<ProductResponseDto>> GetProductByNameAsync(string name)
        {
            var product = await _unitOfWork.Products.GetProductByName(name);
            if (product == null)
            {
                return new ResponseModel<ProductResponseDto>
                {
                    Message = $"Không tìm thấy sản phẩm {name}",
                    Success = false,
                    Data = null
                };
            }
            return new ResponseModel<ProductResponseDto>
            {
                Message = "Cập nhật sản phẩm thành công",
                Success = true,
                Data = new ProductResponseDto
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    Description = product.Description,
                    UnitPrice = product.UnitPrice,
                    StockQuantity = product.StockQuantity,
                    Category = product.Category,
                    IsActive = product.IsActive
                }
            };
        }

        public async Task UpdateInventory(List<OrderDetail> orderDetails)
        {
            // 🔹 Cập nhật hàng tồn kho
            foreach (var detail in orderDetails)
            {
                var product = await _unitOfWork.Products.GetByIdAsync(detail.ProductId);
                if (product == null)
                    throw new BusinessException($"Sản phẩm ID {detail.ProductId} không tồn tại!");

                product.ReduceStock(detail.Quantity);
                await _unitOfWork.Products.UpdateAsync(product);
            }
        }

        public async Task<ResponseModel<ProductResponseDto>> UpdateProductAsync(UpdateProductDto productDto)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var existingProduct = await _unitOfWork.Products.GetByIdAsync(productDto.ProductId);
                if (existingProduct == null)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return new ResponseModel<ProductResponseDto>
                    {
                        Message = $"Không tìm thấy sản phẩm {productDto.ProductName}",
                        Success = false
                    };
                }

                // ✨ Cập nhật thông tin sản phẩm (Map từ DTO sang Entity)
                existingProduct.UpdateInfo(
                    productDto.ProductName,
                    productDto.Description,
                    productDto.UnitPrice,
                    productDto.Category
                );
                // Gọi Repo để cập nhật
                await _unitOfWork.Products.ChangeActiveProduct(productDto.ProductId, productDto.IsActive);
                await _unitOfWork.Products.ChangeQuantityProduct(productDto.ProductId,productDto.StockQuantity);
                await _unitOfWork.Products.UpdateAsync(existingProduct);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();

                return new ResponseModel<ProductResponseDto>
                {
                    Message = "Cập nhật sản phẩm thành công",
                    Success = true,
                    Data = new ProductResponseDto
                    {
                        ProductId = existingProduct.ProductId,
                        ProductName = existingProduct.ProductName,
                        Description = existingProduct.Description,
                        UnitPrice = existingProduct.UnitPrice,
                        StockQuantity = existingProduct.StockQuantity,
                        Category = existingProduct.Category,
                        IsActive = existingProduct.IsActive
                    }
                };
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return new ResponseModel<ProductResponseDto>
                {
                    Message = ex.Message,
                    Success = false
                };
            }
        }


    }
}

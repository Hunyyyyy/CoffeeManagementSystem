using Application__CaféManagementSystem.Application_.DTOs.Orders;
using Application__CaféManagementSystem.Application_.DTOs.Products;
using Application__CaféManagementSystem.Application_.Interface;
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
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IProductService _productService;
        private readonly IUnitofWork _unitOfWork;
        public OrderDetailService(IProductService productService, IUnitofWork unitOfWork)
        {
            _productService = productService;
            _unitOfWork = unitOfWork;
        }
        public async Task<decimal> CalculateTotalAmount(List<OrderDetailCreateDto> orderDetails)
        {
            decimal totalAmount = 0m;
            foreach (var orderDetail in orderDetails)
            {
                foreach (var item in orderDetail.Products)
                {
                    var product = await _productService.GetProductByIdForServiceAsync(item.ProductId)
                        ?? throw new BusinessException($"Sản phẩm ID {item.ProductId} không tồn tại!");

                    decimal unitPrice = product.UnitPrice;
                    decimal subTotal = (unitPrice * item.Quantity) - ((item.Discount / 100) * (unitPrice * item.Quantity));
                    totalAmount += subTotal;
                }
            }
            return totalAmount;
        }
        // 🔹 Thêm chi tiết đơn hàng
        public async Task<List<OrderDetail>> CreateOrderDetails(List<OrderDetailCreateDto> orderDetails, int orderId)
        {
            List<OrderDetail> orderDetailsList = new List<OrderDetail>();
            foreach (var detail in orderDetails)
            {
                foreach (var item in detail.Products)
                {
                    // Lấy sản phẩm từ DB theo ProductId
                    var product = await _productService.GetProductByIdForServiceAsync(item.ProductId)
                        ?? throw new BusinessException($"Sản phẩm ID {item.ProductId} không tồn tại!");

                    // Tạo đối tượng OrderDetail
                    var orderDetail = new OrderDetail(orderId, product.ProductId, item.Quantity, product.UnitPrice, item.Discount);

                    // Thêm OrderDetail vào DB ngay lập tức
                    await _unitOfWork.Orders.AddOrderDetailAsync(orderDetail);
                    orderDetailsList.Add(orderDetail);// Gọi thêm trực tiếp vào DB
                }
            }
            return orderDetailsList.ToList();
        }
    }
}

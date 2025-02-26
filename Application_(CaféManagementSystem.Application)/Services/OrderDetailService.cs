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
                
                    var product = await _productService.GetProductByIdForServiceAsync(orderDetail.ProductId)
                        ?? throw new BusinessException($"Sản phẩm ID {orderDetail.ProductId} không tồn tại!");

                    decimal unitPrice = product.UnitPrice;
                    decimal subTotal = (unitPrice * orderDetail.Quantity) - ((orderDetail.Discount / 100) * (unitPrice * orderDetail.Quantity));
                    totalAmount += subTotal;
                
            }
            return totalAmount;
        }
        // 🔹 Thêm chi tiết đơn hàng
        public async Task<List<OrderDetail>> CreateOrderDetails(List<OrderDetailCreateDto> orderDetails, int orderId)
        {
            List<OrderDetail> orderDetailsList = new List<OrderDetail>();
            foreach (var detail in orderDetails)
            {
               
                    // Lấy sản phẩm từ DB theo ProductId
                    var product = await _productService.GetProductByIdForServiceAsync(detail.ProductId)
                        ?? throw new BusinessException($"Sản phẩm ID {detail.ProductId} không tồn tại!");

                    // Tạo đối tượng OrderDetail
                    var orderDetail = new OrderDetail(orderId, product.ProductId, detail.Quantity, product.UnitPrice, detail.Discount);

                    // Thêm OrderDetail vào DB ngay lập tức
                    await _unitOfWork.Orders.AddOrderDetailAsync(orderDetail);
                    orderDetailsList.Add(orderDetail);// Gọi thêm trực tiếp vào DB
                
            }
            return orderDetailsList.ToList();
        }
    }
}

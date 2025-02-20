using Application__CaféManagementSystem.Application_.DTOs.Invoices;
using Application__CaféManagementSystem.Application_.DTOs.Orders;
using Application__CaféManagementSystem.Application_.DTOs.Products;
using Application__CaféManagementSystem.Application_.Helpers;
using Application__CaféManagementSystem.Application_.Interface;
using Application__CaféManagementSystem.Application_.Models;
using Core_CaféManagementSystem.Core.Entities;
using Core_CaféManagementSystem.Core.Exceptions;
using Core_CaféManagementSystem.Core.Interface;
using Infrastructure__CaféManagementSystem.Infrastructure_.Data.UnitofWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core_CaféManagementSystem.Core.Common.Enums;

namespace Application_CaféManagementSystem.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitofWork _unitOfWork;
        private readonly IProductService _productService;
        private readonly ITableService _tableService;
        private readonly IPaymentService _paymentService;
        private readonly IInvoiceService _invoiceService;
        private readonly IOrderDetailService _orderDetailService;
        public OrderService(IUnitofWork unitOfWork, IProductService productService,
            ITableService tableService, IPaymentService paymentService,
            IInvoiceService invoiceService, IOrderDetailService orderDetailService)
        {
            _unitOfWork = unitOfWork;
            _productService = productService;
            _tableService = tableService;
            _paymentService = paymentService;
            _invoiceService = invoiceService;
            _orderDetailService = orderDetailService;
        }



        public async Task<ResponseModel<OrderResposeDto>> CreateOrderAsync(OrderCreateDto orderDto)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                // 🔹 Kiểm tra dữ liệu đầu vào
                var t = await _unitOfWork.CoffeeTables.ExistsAsync(orderDto.TableId);
                if (!t)
                    return ResponseFactory.NotFound<OrderResposeDto>($"Bàn không tồn tại!");
                if (!await _unitOfWork.Employees.ExistsAsync(orderDto.EmployeeId))
                    return ResponseFactory.NotFound<OrderResposeDto>($"Nhân viên không tồn tại!");
                // 🔹 Kiểm tra tồn kho trước
                foreach (var orderDetail in orderDto.OrderDetails)
                {
                    foreach (var item in orderDetail.Products)
                    {
                        var product = await _unitOfWork.Products.GetByIdAsync(item.ProductId);
                        if (product == null)
                            return ResponseFactory.NotFound<OrderResposeDto>($"Sản phẩm có {item.ProductId} không tồn tại!");
                        
                            
                        if (product.StockQuantity < item.Quantity)
                            return ResponseFactory.NotFound<OrderResposeDto>($"Sản phẩm của {product.ProductName} không đủ để Order,tạo đơn hàng thất bại");
                            
                    }
                }
                // 🔹 Sau khi kiểm tra xong => tính tổng tiền
                decimal totalAmount = await _orderDetailService.CalculateTotalAmount(orderDto.OrderDetails);

                // 🔹 Tạo đơn hàng Entities
                var order = new Order(orderDto.TableId, orderDto.EmployeeId, totalAmount, OrderStatus.Pending);
                await _unitOfWork.Orders.AddAsync(order);
                await _unitOfWork.SaveChangesAsync();

                // 🔹 Thêm chi tiết đơn hàng
                await _orderDetailService.CreateOrderDetails(orderDto.OrderDetails, order.OrderId);
                await _unitOfWork.SaveChangesAsync();
                // 🔹 Cập nhật hàng tồn kho
                await _productService.UpdateInventory(order.OrderDetails.ToList());
                await _unitOfWork.SaveChangesAsync();
                // 🔹 Tạo hóa đơn
                var invoice =  await _invoiceService.CreateInvoiceAsync(order.OrderId, totalAmount, PaymentStatus.Unpaid);
                await _unitOfWork.SaveChangesAsync();
                // 🔹 Tạo payment
                 await _paymentService.CreatePaymentsAsync(orderDto.InvoiceCreateDto.Payments);
                // 🔹 cập nhật trạng thái bàn
                await _tableService.UpdateTableStatusAsync(orderDto.TableId, CoffeeTableStatus.Occupied);
                await _unitOfWork.SaveChangesAsync();
                // 🔹 Cập nhật PTTT cho hóa đơn
                foreach (var payment in orderDto.InvoiceCreateDto.Payments)
                {
                    await _invoiceService.UpdateStatusInvoice(payment.Amount, totalAmount, invoice);
                    await _unitOfWork.SaveChangesAsync();
                }
                // 🔹 Lưu dữ liệu và commit transaction
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();
                // 🔹 Trả về đơn hàng đã tạo
                // 🔹 Tạo OrderResponseDto
                // Lấy TableNumber từ dịch vụ
                var table = await _tableService.GetCoffeeTableByIdAsync(orderDto.TableId);

                // Lấy tên sản phẩm cho từng sản phẩm trong OrderDetails
                var orderDetailsResponse = new List<OrderDetailResponseDto>();
                foreach (var orderDetail in orderDto.OrderDetails)
                {
                    foreach (var p in orderDetail.Products)
                    {
                        var product = await _unitOfWork.Products.GetByIdAsync(p.ProductId);
                        if (product != null)
                        {
                            orderDetailsResponse.Add(new OrderDetailResponseDto
                            {
                                ProductName = product.ProductName,
                                Quantity = p.Quantity,
                                UnitPrice = product.UnitPrice,
                                Discount = p.Discount
                            });
                        }
                    }
                }

                // Tạo OrderResponseDto
                var orderResponseDto = new OrderResposeDto
                {
                    OrderId = order.OrderId,
                    TableNumber = table.TableNumber,  // Lấy từ dịch vụ
                    OrderDate = order.OrderDate,
                    TotalAmount = totalAmount,
                    Status = order.Status.ToString(),
                    OrderDetails = orderDetailsResponse,
                    Invoice = new InvoiceResponseDto
                    {
                        TotalAmount = invoice.TotalAmount,
                        PaymentStatus = invoice.PaymentStatus.ToString(),
                    }
                };
                return ResponseFactory.Success(orderResponseDto,"Tạo đơn hàng thành công");
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                return ResponseFactory.Fail<OrderResposeDto>("Tạo đơn hàng thất bại!");
            }
        }
       
        public  Task<Order> CancelOrderAsync(int id)
        {
          throw new NotImplementedException();
        }


        public Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetOrderByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Order>> GetStatusOrdersAsync()
        {
            var orders = await _unitOfWork.Orders.GetStatusOrdersAsync();
            return orders;
        }

        public Task<Order> UpdateOrderAsync(Order order)
        {
            throw new NotImplementedException();
        }
    }
}

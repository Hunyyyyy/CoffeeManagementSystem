using Application__CaféManagementSystem.Application_.DTOs.Invoices;
using Application__CaféManagementSystem.Application_.DTOs.Orders;
using Application__CaféManagementSystem.Application_.DTOs.Payments;
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

        //chưa tối ưu mã
        public async Task<ResponseModel<OrderResposeDto>> ConfirmOrderAsync(EmployeeConfirmOrderDto orderDto)
        {
            if(orderDto.EmployeeId==12)
                return ResponseFactory.Fail<OrderResposeDto>("Nhân viên không tồn tại");
            if (orderDto == null)
                return ResponseFactory.Fail<OrderResposeDto>("Dữ liệu đơn hàng không hợp lệ");

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var order = await _unitOfWork.Orders.GetByIdAsync(orderDto.OrderId);
                if (order == null)
                    return ResponseFactory.NotFound<OrderResposeDto>("Đơn hàng không tồn tại");

                // ❗ Kiểm tra nếu trạng thái mới trùng với trạng thái cũ
                if (order.Status == orderDto.OrderStatus)
                {
                    return ResponseFactory.FailWithData(new OrderResposeDto
                    {
                        OrderId = order.OrderId,
                        TableNumber = await _tableService.GetTableNumberByIdAsync(order.TableId),
                        OrderDate = order.OrderDate,
                        TotalAmount = order.TotalAmount,
                        Status = order.Status.ToString(),
                        OrderDetails = order.OrderDetails.Select(od => new OrderDetailResponseDto
                        {
                            ProductId = od.ProductId,
                            ProductName = od.Product?.ProductName,
                            Quantity = od.Quantity,
                            UnitPrice = od.UnitPrice,
                            Discount = od.Discount
                        }).ToList()
                    }, "Trạng thái giống nhau, không thể thay đổi");
                }

                // ✅ Cập nhật trạng thái đơn hàng
                await UpdateOrderStatusAsync(order, orderDto.OrderStatus);

                // ✅ Cập nhật EmployeeId
                order.UpdateEmployeeId(orderDto.EmployeeId);

                await _unitOfWork.SaveChangesAsync(); // Không cần UpdateAsync(order) vì entity đã được theo dõi

                // ✅ Map kết quả trả về
                var MapOrder = new OrderResposeDto
                {
                    OrderId = order.OrderId,
                    TableNumber = await _tableService.GetTableNumberByIdAsync(order.TableId),
                    OrderDate = order.OrderDate,
                    TotalAmount = order.TotalAmount,
                    Status = order.Status.ToString(),
                    OrderDetails = order.OrderDetails.Select(od => new OrderDetailResponseDto
                    {
                        ProductId = od.ProductId,
                        ProductName = od.Product?.ProductName,
                        Quantity = od.Quantity,
                        UnitPrice = od.UnitPrice,
                        Discount = od.Discount
                    }).ToList()
                };

                await _unitOfWork.CommitTransactionAsync();
                return ResponseFactory.Success(MapOrder, "Trạng thái được cập nhật thành công");
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return ResponseFactory.Fail<OrderResposeDto>($"Lỗi xử lý đơn hàng: {ex.Message}");
            }
        }


        private async Task<OrderStatus> UpdateOrderStatusAsync(Order order, OrderStatus newStatus)
        {
            if (!Enum.IsDefined(typeof(OrderStatus), newStatus))
                throw new ArgumentOutOfRangeException(nameof(newStatus), newStatus, "Trạng thái đơn hàng không hợp lệ");
            if(order.Status == OrderStatus.Cancelled)
                throw new BusinessException("Đơn hàng đã bị hủy, không thể cập nhật trạng thái");
            if (order.Status == OrderStatus.Completed)
                throw new BusinessException("Đơn hàng đã hoàn thành, không thể cập nhật trạng thái");
            order.ChangeStatus(newStatus);
            await _unitOfWork.SaveChangesAsync();

            return order.Status; // Trả về trạng thái mới sau khi cập nhật
        }


        public Task<Order> CancelOrderAsync(int id)
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

        public async Task<ResponseModel<OrderResposeDto>> SendOrderFromCustomerToEmployee(
            OrderCreateDto orderCreateDto)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                // 🔹 Kiểm tra bàn có tồn tại không
                if (!await _unitOfWork.CoffeeTables.ExistsAsync(orderCreateDto.TableId))
                    return ResponseFactory.NotFound<OrderResposeDto>("Bàn không tồn tại!");

                // 🔹 Kiểm tra và cập nhật tồn kho
                var validationResult = await _productService.ValidateAndUpdateStockAsync(orderCreateDto.OrderDetails);
                if (!validationResult.Success)
                    return ResponseFactory.Fail<OrderResposeDto>(validationResult.Message ?? "Cập nhật Thất bại");
                await _unitOfWork.SaveChangesAsync();
                // 🔹 Tính tổng tiền đơn hàng
                decimal totalAmount = await _orderDetailService.CalculateTotalAmount(orderCreateDto.OrderDetails);
                // 🔹 Tạo đơn hàng
                var order = new Order(orderCreateDto.TableId,12, totalAmount, OrderStatus.Pending);
                if (order == null)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return ResponseFactory.Fail<OrderResposeDto>("Tạo đơn hàng thất bại");
                }
                await _unitOfWork.Orders.AddAsync(order);
                await _unitOfWork.SaveChangesAsync();
                // 🔹 Tạo chi tiết đơn hàng
                var orderDetails = await _orderDetailService.CreateOrderDetails(orderCreateDto.OrderDetails, order.OrderId);
                if (orderDetails == null)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return ResponseFactory.Fail<OrderResposeDto>("Tạo chi tiết đơn hàng thất bại");
                }
                // 🔹 Tạo hóa đơn
                var paymentStatus = orderCreateDto.PaymentCreateDto.PaymentMethod == PaymentMethod.Cash ? PaymentStatus.Unpaid : PaymentStatus.Paid;
                var invoice = await _invoiceService.CreateInvoiceAsync(order.OrderId, totalAmount, paymentStatus);
                if (invoice == null)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return ResponseFactory.Fail<OrderResposeDto>("Tạo hóa đơn thất bại");
                }
                await _unitOfWork.SaveChangesAsync();
                // 🔹 Xử lý thanh toán
                var paymentResult = await _paymentService.CreatePaymentsAsync(orderCreateDto.PaymentCreateDto, invoice.InvoiceId);
                if (paymentResult == null)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return ResponseFactory.Fail<OrderResposeDto>("Tạo thanh toán thất bại");
                }

                // 🔹 Cập nhật trạng thái bàn
                await _tableService.UpdateTableStatusAsync(orderCreateDto.TableId, CoffeeTableStatus.Occupied);
                await _unitOfWork.SaveChangesAsync();
                if (orderCreateDto.PaymentCreateDto.Amount < totalAmount || orderCreateDto.PaymentCreateDto.Amount > totalAmount && !(orderCreateDto.PaymentCreateDto.PaymentMethod == PaymentMethod.Cash))
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return ResponseFactory.Fail<OrderResposeDto>("Số tiền thanh toán không hợp lệ");
                }
                // 🔹 Chuyển đổi danh sách OrderDetail thành OrderDetailResponseDto
                var orderDetailsResponse = orderDetails.Select(od => new OrderDetailResponseDto
                {
                    ProductId = od.ProductId,
                    ProductName = od.Product?.ProductName, // Giả sử có navigation property
                    Quantity = od.Quantity,
                    UnitPrice = od.UnitPrice,
                    Discount = od.Discount,

                }).ToList();
                var mapInvoice = new InvoiceResponseDto
                {
                    PaymentStatus = invoice.PaymentStatus.ToString(),
                    TotalAmount = invoice.TotalAmount,
                    Payment = new PaymentResponseDto
                    {
                        Amount = paymentResult.Amount,
                        PaymentMethod = paymentResult.PaymentMethod.ToString(),
                        TransactionCode = paymentResult.TransactionCode
                    }

                };
                var orderResposeDto = new OrderResposeDto
                {
                    OrderId = order.OrderId,
                    TableNumber = await _tableService.GetTableNumberByIdAsync(orderCreateDto.TableId),
                    OrderDate = order.OrderDate,
                    TotalAmount = order.TotalAmount,
                    Status = order.Status.ToString(),
                    Invoice = mapInvoice,
                    OrderDetails = orderDetailsResponse
                };
                await _unitOfWork.CommitTransactionAsync();
                return ResponseFactory.Success(orderResposeDto, "Tạo đơn hàng thành công");
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return ResponseFactory.Fail<OrderResposeDto>($"Lỗi xử lý đơn hàng: {ex.Message}");
            }
        }

       

    }
}

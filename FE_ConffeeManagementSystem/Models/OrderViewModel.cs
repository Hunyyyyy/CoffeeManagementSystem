using Application__CaféManagementSystem.Application_.DTOs.Invoices;

namespace FE_ConffeeManagementSystem.Models
{
    public class OrderViewModel
    {
        public int TableId { get; set; }
        public int EmployeeId { get; set; }

        public int OrderId { get; set; } // ID của Order (nếu cần)

        public List<PaymentCreateDto> Payments { get; set; } = new();

        public List<OrderDetailViewModel> OrderDetails { get; set; } = new();
    }

    public class OrderDetailViewModel
    {
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
        public List<int> ProductIds { get; set; } = new(); // Lưu danh sách ID sản phẩm
    }
}

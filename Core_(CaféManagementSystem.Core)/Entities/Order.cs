using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core_CaféManagementSystem.Core.Common.Enums;

namespace Core_CaféManagementSystem.Core.Entities
{
    public class Order
    {
        public int OrderId { get; private set; }  // Khóa chính
        public int TableId { get; private set; } // Khóa ngoại đến CoffeeTable
        public int EmployeeId { get; private set; } // Khóa ngoại đến Employee
        public DateTime OrderDate { get; private set; } // Ngày đặt hàng
        public decimal TotalAmount { get; private set; } // Tổng tiền đơn hàng
        [Column(TypeName = "nvarchar(50)")]
        public OrderStatus Status { get; private set; } = OrderStatus.Pending; // Lưu enum dưới dạng chuỗi
        public virtual Employee? Employee { get; set; }
        public virtual CoffeeTable? Table { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
        // Constructor để tạo đơn hàng mới
        public Order(int tableId, int employeeId, decimal totalAmount, OrderStatus status)
        {
            if (totalAmount < 0)
                throw new ArgumentException("Tổng tiền không thể âm!");

            TableId = tableId;
            EmployeeId = employeeId;
            OrderDate = DateTime.UtcNow;
            TotalAmount = totalAmount;
            Status = status;
        }

        // Thêm chi tiết đơn hàng sau khi tạo đơn hàng
        public void AddOrderDetail(OrderDetail detail)
        {
            if (detail == null) throw new ArgumentNullException(nameof(detail));
            OrderDetails.Add(detail);
        }


        // Hành vi: Cập nhật tổng tiền
        public void UpdateTotalAmount(decimal totalAmount)
        {
            if (totalAmount < 0)
                throw new ArgumentException("Tổng tiền không thể âm!");

            TotalAmount = totalAmount;
        }

        // Hành vi: Thay đổi trạng thái đơn hàng
        public void ChangeStatus(OrderStatus newStatus) => Status = newStatus;
        public void UpdateEmployeeId(int employeeId) => EmployeeId = employeeId;
    }

}

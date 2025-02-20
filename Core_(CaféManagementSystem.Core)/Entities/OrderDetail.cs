using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_CaféManagementSystem.Core.Entities
{
    public class OrderDetail
    {
        public int OrderDetailId { get; private set; }  // Khóa chính
        public int OrderId { get; private set; } // Khóa ngoại đến Orders
        public int ProductId { get; private set; } // Khóa ngoại đến Products
        public int Quantity { get; private set; } // Số lượng sản phẩm
        public decimal UnitPrice { get; private set; } // Giá mỗi sản phẩm
        public decimal Discount { get; private set; } // Chiết khấu (mặc định = 0)
        public virtual Order? Order { get; set; }
        public virtual Product? Product { get; set; }
        // Constructor để thêm chi tiết đơn hàng mới
        public OrderDetail(int orderId, int productId, int quantity, decimal unitPrice, decimal discount = 0)
        {
            if (quantity <= 0)
                throw new ArgumentException("Số lượng phải lớn hơn 0!");

            if (unitPrice < 0)
                throw new ArgumentException("Giá sản phẩm không thể âm!");

            if (discount < 0)
                throw new ArgumentException("Chiết khấu không thể âm!");

            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Discount = discount;
        }

        // Hành vi: Cập nhật số lượng sản phẩm trong chi tiết đơn hàng
        public void UpdateQuantity(int newQuantity)
        {
            if (newQuantity <= 0)
                throw new ArgumentException("Số lượng phải lớn hơn 0!");

            Quantity = newQuantity;
        }

        // Hành vi: Cập nhật giá sản phẩm
        public void UpdateUnitPrice(decimal newUnitPrice)
        {
            if (newUnitPrice < 0)
                throw new ArgumentException("Giá sản phẩm không thể âm!");

            UnitPrice = newUnitPrice;
        }

        // Hành vi: Cập nhật chiết khấu
        public void UpdateDiscount(decimal newDiscount)
        {
            if (newDiscount < 0)
                throw new ArgumentException("Chiết khấu không thể âm!");

            Discount = newDiscount;
        }

        // Hành vi: Tính tổng tiền của mục chi tiết đơn hàng
        public decimal CalculateTotalPrice()
        {
            return (UnitPrice * Quantity) - Discount;
        }
    }

}

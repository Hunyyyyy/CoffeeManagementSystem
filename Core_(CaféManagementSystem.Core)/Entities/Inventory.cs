using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_CaféManagementSystem.Core.Entities
{
    public class Inventory
    {
        public int InventoryId { get; private set; }  // Khóa chính
        public int ProductId { get; private set; } // Khóa ngoại tham chiếu đến Products
        public int Quantity { get; private set; } // Số lượng tồn kho
        public DateTime LastUpdated { get; private set; } // Thời gian cập nhật lần cuối
        public virtual Product? Product { get; set; }
        // Constructor để khởi tạo tồn kho mới
        public Inventory(int productId, int quantity)
        {
            if (quantity < 0)
                throw new ArgumentException("Số lượng tồn kho không thể âm!");

            ProductId = productId;
            Quantity = quantity;
            LastUpdated = DateTime.UtcNow;
        }

        // Hành vi: Cập nhật số lượng tồn kho
        public void UpdateQuantity(int newQuantity)
        {
            if (newQuantity < 0)
                throw new ArgumentException("Số lượng tồn kho không thể âm!");

            Quantity = newQuantity;
            LastUpdated = DateTime.UtcNow;
        }

        // Hành vi: Nhập hàng (Tăng số lượng tồn kho)
        public void AddStock(int amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Số lượng nhập phải lớn hơn 0!");

            Quantity += amount;
            LastUpdated = DateTime.UtcNow;
        }

        // Hành vi: Xuất hàng (Giảm số lượng tồn kho)
        public void RemoveStock(int amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Số lượng xuất phải lớn hơn 0!");

            if (amount > Quantity)
                throw new InvalidOperationException("Không đủ hàng trong kho!");

            Quantity -= amount;
            LastUpdated = DateTime.UtcNow;
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_CaféManagementSystem.Core.Entities
{
    public class Product
    {
        public int ProductId { get; private set; }  // Khóa chính
        public string ProductName { get; private set; } // Tên sản phẩm
        public string? Description { get; private set; } // Mô tả sản phẩm (có thể null)
        public decimal UnitPrice { get; private set; } // Giá sản phẩm
        public int StockQuantity { get; private set; } // Số lượng tồn kho
        public string? Category { get; private set; } // Danh mục sản phẩm (có thể null)
        public bool IsActive { get; private set; } = true; // Trạng thái sản phẩm

        // Constructor để khởi tạo sản phẩm mới
        public Product(string productName, string? description, decimal unitPrice, int stockQuantity, string? category)
        {
            if (unitPrice <= 0)
                throw new ArgumentException("Giá sản phẩm phải lớn hơn 0!");

            if (stockQuantity < 0)
                throw new ArgumentException("Số lượng tồn kho không được âm!");

            ProductName = productName;
            Description = description;
            UnitPrice = unitPrice;
            StockQuantity = stockQuantity;
            Category = category;
            IsActive = true;
        }

        // Hành vi: Cập nhật thông tin sản phẩm
        public void UpdateInfo(string productName, string? description, decimal unitPrice, string? category)
        {
            if (unitPrice <= 0)
                throw new ArgumentException("Giá sản phẩm phải lớn hơn 0!");

            ProductName = productName;
            Description = description;
            UnitPrice = unitPrice;
            Category = category;
        }

        // Hành vi: Thêm hàng vào kho
        public void AddStock(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Số lượng nhập phải lớn hơn 0!");

            StockQuantity += quantity;
        }

        // Hành vi: Trừ hàng trong kho (khi bán)
        public void ReduceStock(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Số lượng bán phải lớn hơn 0!");

            if (StockQuantity < quantity)
                throw new InvalidOperationException("Không đủ hàng trong kho!");

            StockQuantity -= quantity;
        }

        // Hành vi: Vô hiệu hóa sản phẩm
        public void Deactivate()
        {
            IsActive = false;
        }

        // Hành vi: Kích hoạt lại sản phẩm
        public void Activate()
        {
            IsActive = true;
        }
    }

}

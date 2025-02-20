using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_CaféManagementSystem.Core.Common
{
    public class Enums
    {
        public enum OrderStatus
        {
            Pending=0,     // Đang chờ xác nhận
            Confirmed=1,   // Đã xác nhận
            Processing=2,  // Đang chuẩn bị
            Completed=3,   // Đã hoàn thành
            Cancelled=4    // Đã hủy
        }

        public enum PaymentStatus
        {
            Unpaid=0,     // Chưa thanh toán
            Paid=1,       // Đã thanh toán
            Partial=2     // Thanh toán một phần
        }

        public enum PaymentMethod
        {
            Cash = 0,           // Tiền mặt
            CreditCard =1,     // Thẻ tín dụng
            BankTransfer=2,   // Chuyển khoản ngân hàng
            EWallet=3        // Ví điện tử
        }
        public enum CoffeeTableStatus
        {
            Available = 0,  // Có thể sử dụng
            Occupied = 1,   // Đã có khách
            Reserved =2    // Đã đặt trước
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_CaféManagementSystem.Core.Common
{
    //Dùng để tái sử dụng các xử lý chung thay vì lặp lại mã nhiều lần.
    // Ví dụ 1: Xử lý định dạng tiền tệ
    public static class Helpers
    {
        // 🔹 Định dạng tiền VNĐ (VD: 260,000 VNĐ)
        public static string FormatCurrency(decimal amount)
        {
            return string.Format("{0:N0} VNĐ", amount); // Định dạng số nguyên, có dấu phân cách hàng nghìn
        }

        // 🔹 Định dạng ngày thành "dd/MM/yyyy" (VD: 25/02/2025)
        public static string FormatDate(DateTime date)
        {
            return date.ToString("dd/MM/yyyy");
        }

        // 🔹 Định dạng ngày có giờ phút giây (VD: 25/02/2025 14:30:45)
        public static string FormatDateTime(DateTime date)
        {
            return date.ToString("dd/MM/yyyy HH:mm:ss");
        }
    }
}

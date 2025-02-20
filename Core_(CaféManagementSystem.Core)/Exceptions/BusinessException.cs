using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_CaféManagementSystem.Core.Exceptions
{
   public class BusinessException : Exception// Lỗi logic nghiệp vụ
    {
        // Mã lỗi (Nếu có)
        public string? ErrorCode { get; }

        // Thời gian lỗi xảy ra
        public DateTime ErrorTime { get; }

        // Một thuộc tính bổ sung để cung cấp thông tin chi tiết
        public string? Detail { get; }

        // Constructor cơ bản nhận thông điệp lỗi
        public BusinessException(string message) : base(message)
        {

            ErrorTime = DateTime.UtcNow;
        }

        // Constructor nhận thông điệp và mã lỗi
        public BusinessException(string message, string errorCode) : base(message)
        {
            ErrorCode = errorCode;
            ErrorTime = DateTime.UtcNow;
        }

        // Constructor với thông điệp, mã lỗi và các chi tiết lỗi
        public BusinessException(string message, string errorCode, string detail) : base(message)
        {
            ErrorCode = errorCode;
            ErrorTime = DateTime.UtcNow;
            Detail = detail;
        }

        // Constructor với thông điệp và inner exception (khi bạn muốn ném một ngoại lệ bên trong một ngoại lệ khác)
        public BusinessException(string message, Exception innerException) : base(message, innerException)
        {
            ErrorTime = DateTime.UtcNow;
        }

    }
}

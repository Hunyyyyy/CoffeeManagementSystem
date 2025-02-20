using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_CaféManagementSystem.Core.Exceptions
{
   public class ValidationException : Exception// Lỗi khi dữ liệu không hợp lệ
    {
        public IDictionary<string, string[]> Errors { get; }

        public ValidationException(string message) : base(message)
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(IDictionary<string, string[]> errors)
            : base("Dữ liệu không hợp lệ.")
        {
            Errors = errors;
        }
    }
}

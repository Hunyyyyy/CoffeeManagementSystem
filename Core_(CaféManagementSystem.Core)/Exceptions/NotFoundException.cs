using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_CaféManagementSystem.Core.Exceptions
{
   public class NotFoundException : Exception// Lỗi khi không tìm thấy dữ liệu
    {
        public NotFoundException(string message) : base(message)
        {

        }
        public NotFoundException(string entityName, object key)
        : base($"{entityName} với ID = {key} không được tìm thấy.") { }
    }
}

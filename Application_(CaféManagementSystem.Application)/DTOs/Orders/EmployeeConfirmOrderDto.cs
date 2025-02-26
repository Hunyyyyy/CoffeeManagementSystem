using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core_CaféManagementSystem.Core.Common.Enums;

namespace Application__CaféManagementSystem.Application_.DTOs.Orders
{
    public class EmployeeConfirmOrderDto
    {
        public int OrderId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public int EmployeeId { get; set; }
    }
}

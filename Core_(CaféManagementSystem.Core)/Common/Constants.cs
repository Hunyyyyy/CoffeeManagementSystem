using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_CaféManagementSystem.Core.Common
{
    public static class Constants
    {

        public static Dictionary<string,decimal> RoleBaseSalary = new Dictionary<string, decimal>
        {
            { "Quản lý", 5000000 },
            { "Nhân viên phục vụ", 2000000 },
            { "Nhân viên pha chế", 3500000 },
            { "Nhân viên thu ngân", 4000000 }
        };
        public static Dictionary<int,string> RoleMappings = new Dictionary<int, string>
        {
            { 1, "Quản lý" },
            { 2, "Nhân viên phục vụ" },
            { 3, "Nhân viên pha chế" },
            { 4, "Nhân viên thu ngân" }
        };
    }
}

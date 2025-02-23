﻿using Core__CaféManagementSystem.Core_.Interface;
using Core_CaféManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_CaféManagementSystem.Core.Interface
{
    public interface IUserRepository : RepositoriesBase<User>
    {
        Task<User?> GetByUsernameAsync(string username);
        Task<string?> GetRoleNameByIdAsync(int roleId);
    }
}

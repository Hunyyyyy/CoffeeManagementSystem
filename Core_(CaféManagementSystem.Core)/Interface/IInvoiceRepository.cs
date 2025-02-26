using Core__CaféManagementSystem.Core_.Interface;
using Core_CaféManagementSystem.Core.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_CaféManagementSystem.Core.Interface
{
    public interface IInvoiceRepository : RepositoriesBase<Invoice>
    {
        Task<Invoice> GetInvoiceByOrderIdAsync(int id);
        IQueryable<Invoice> GetPaidInvoices();
        IQueryable<Salary> GetSalariesForCurrentMonth();
    }
}

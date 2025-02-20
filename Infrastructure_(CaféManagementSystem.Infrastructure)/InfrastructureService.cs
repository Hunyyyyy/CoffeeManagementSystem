using Core_CaféManagementSystem.Core.Interface;
using Infrastructure__CaféManagementSystem.Infrastructure_.Data.Repositories;
using Infrastructure__CaféManagementSystem.Infrastructure_.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Infrastructure__CaféManagementSystem.Infrastructure_.Data.UnitofWork;

namespace Infrastructure__CaféManagementSystem.Infrastructure_
{
   public static class InfrastructureService
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Đăng ký DbContext với SQL Server
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            // Đăng ký Repository
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IInvoiceRepository, InvoiceRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<ICoffeeTableRepository, CoffeeTableRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IInventoryRepository, InventoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUnitofWork, UnitofWork>();
            // Đăng ký các repository khác...

            // Đăng ký Unit of Work (nếu có)
            //services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}

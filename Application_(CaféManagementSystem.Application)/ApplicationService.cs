using Application__CaféManagementSystem.Application_.Interface;
using Application__CaféManagementSystem.Application_.Services;
using Application_CaféManagementSystem.Application.Services;
using Core_CaféManagementSystem.Core.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application__CaféManagementSystem.Application_
{
    public static class ApplicationService
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Đăng ký các dịch vụ ứng dụng
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IInvoiceService, InvoiceService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ITableService, CoffeeTableService>();
            services.AddScoped<IOrderDetailService, OrderDetailService>();
            // services.AddAutoMapper(typeof(ApplicationService));



            // Đăng ký các dịch vụ khác...
            return services;
        }
    }
}

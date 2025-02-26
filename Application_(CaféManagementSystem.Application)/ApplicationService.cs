using Application__CaféManagementSystem.Application_.Interface;
using Application__CaféManagementSystem.Application_.Models;
using Application__CaféManagementSystem.Application_.Provider;
using Application__CaféManagementSystem.Application_.Services;
using Application_CaféManagementSystem.Application.Services;
using Core_CaféManagementSystem.Core.Entities;
using Core_CaféManagementSystem.Core.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IJwtProvider, JwtProvider>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISalaryService, SalaryService>();
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IRevenueAndReportService, RevenueAndReportService>();

            //JwtSettings là configuration section, không phải một service.
            services.Configure<JwtSettings>(configuration.GetSection("Jwt"));
            // services.AddAutoMapper(typeof(ApplicationService));



            // Đăng ký các dịch vụ khác...
            return services;
        }
    }
}

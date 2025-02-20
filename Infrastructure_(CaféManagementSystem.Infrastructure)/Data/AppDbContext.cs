using Core__CaféManagementSystem.Core_.Entities;
using Core_CaféManagementSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core_CaféManagementSystem.Core.Common.Enums;

namespace Infrastructure__CaféManagementSystem.Infrastructure_.Data
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Khai báo DbSet cho từng bảng
        public DbSet<Employee> Employees { get; set; }
        public DbSet<CoffeeTable> CoffeeTables { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<InventoryLog> InventoryLogs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Salary> Salaries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Thiết lập quan hệ
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Table)
                .WithMany()
                .HasForeignKey(o => o.TableId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Employee)
                .WithMany()
                .HasForeignKey(o => o.EmployeeId);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Product)
                .WithMany()
                .HasForeignKey(od => od.ProductId);

            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.Order)
                .WithOne()
                .HasForeignKey<Invoice>(i => i.OrderId);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Invoice)
                .WithMany()
                .HasForeignKey(p => p.InvoiceId);

            modelBuilder.Entity<Inventory>()
                .HasOne(i => i.Product)
                .WithMany()
                .HasForeignKey(i => i.ProductId);

            modelBuilder.Entity<InventoryLog>()
                .HasOne(il => il.Product)
                .WithMany()
                .HasForeignKey(il => il.ProductId);

            modelBuilder.Entity<InventoryLog>()
                .HasOne(il => il.Employee)
                .WithMany()
                .HasForeignKey(il => il.EmployeeId);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Employee)
                .WithOne()
                .HasForeignKey<User>(u => u.EmployeeId);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany()
                .HasForeignKey(u => u.RoleId);

            modelBuilder.Entity<Salary>()
                .HasOne(s => s.Employee)
                .WithMany()
                .HasForeignKey(s => s.EmployeeId);
            // Ánh xạ enum thành chuỗi
            modelBuilder.Entity<Order>()
                .Property(o => o.Status)
                .HasConversion(
                    v => v.ToString(),  // Enum -> String (lưu trong DB)
                    v => (OrderStatus)Enum.Parse(typeof(OrderStatus), v)  // String -> Enum (khi đọc từ DB)
                );

            modelBuilder.Entity<Invoice>()
                .Property(i => i.PaymentStatus)
                .HasConversion(
                    v => v.ToString(),  // Enum -> String
                    v => (PaymentStatus)Enum.Parse(typeof(PaymentStatus), v)  // String -> Enum
                );

            modelBuilder.Entity<Payment>()
                .Property(od => od.PaymentMethod)
                .HasConversion(
                    v => v.ToString(),  // Enum -> String
                    v => (PaymentMethod)Enum.Parse(typeof(PaymentMethod), v)  // String -> Enum
                );

            modelBuilder.Entity<CoffeeTable>()
                .Property(ct => ct.Status)
                .HasConversion(
                    v => v.ToString(),  // Enum -> String
                    v => (CoffeeTableStatus)Enum.Parse(typeof(CoffeeTableStatus), v)  // String -> Enum
                );
        }
        }
}

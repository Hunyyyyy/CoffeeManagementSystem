using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using CoffeeManagementSystem.Models;
using Application__CaféManagementSystem.Application_.DTOs.Orders;
using Application__CaféManagementSystem.Application_.DTOs.Invoices;
using Application__CaféManagementSystem.Application_.DTOs.Products;
using Core_CaféManagementSystem.Core.Entities;
using static Core_CaféManagementSystem.Core.Common.Enums;
using FE_ConffeeManagementSystem.Models;

namespace FE_ConffeeManagementSystem.Controllers
{
    public class OrderController : Controller
    {
        private readonly HttpClient _httpClient;
        public OrderController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }
        public IActionResult CreateOrder()
        {
            return View();
        }
        //[HttpPost]
        //public async Task<IActionResult> CreateOrder(OrderViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    var newOrder = new OrderCreateDto
        //    {
        //        TableId = model.TableId,
        //        EmployeeId = model.EmployeeId,
        //        InvoiceCreateDto = new InvoiceCreateDto
        //        {
        //            Payments = model.Payments
        //        },
        //        OrderDetails = model.OrderDetails.Select(detail => new OrderDetailCreateDto
        //        {
        //            Products = detail.ProductIds.Select(id => new GetProductClientDto { ProductId = id }).ToList()
        //        }).ToList()
        //    };

        //    // Gọi API gửi đơn hàng
        //    var response = await _httpClient.PostAsJsonAsync("https://localhost:7058/api/orders", newOrder);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        return RedirectToAction("OrderSuccess");
        //    }

        //    ModelState.AddModelError(string.Empty, "Gửi đơn hàng thất bại.");
        //    return View(model);
        //}

    }
}

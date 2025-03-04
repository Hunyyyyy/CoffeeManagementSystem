using Application__CaféManagementSystem.Application_.DTOs.Products;
using Application__CaféManagementSystem.Application_.Interface;
using Application__CaféManagementSystem.Application_.Models;
using Azure;
using Core_CaféManagementSystem.Core.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CoffeeManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IAuthService _authService;
        public ProductController(IProductService productService, IAuthService authService)
        {
            _productService = productService;
            _authService = authService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Policy = nameof(Enums.Role.Manager))] // Chỉ yêu cầu đăng nhập, không kiểm tra role
        [HttpPost("add")]
        public async Task<IActionResult> AddProduct(CreateProductDto productDto)
        {
            if (productDto == null)
            {
                return BadRequest("Dữ liệu đầu vào không hợp lệ.");
            }

            var product = await _productService.AddProductAsync(productDto);

            return Ok(product);
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetAllProduct()
        {
            var productList = await _productService.GetAllProductsAsync();

            if (!productList.Success)
                return BadRequest(productList); // Trả về HTTP 400 nếu không tìm thấy sản phẩm

            return Ok(productList); // Trả về HTTP 200 với danh sách sản phẩm
        }
        [Authorize(Policy = nameof(Enums.Role.Manager))]
        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> UpdateProdcutAsync(UpdateProductDto productDto)
        {
           
            var product = await _productService.UpdateProductAsync(productDto);
            if (!product.Success)
                return BadRequest(product); // Trả về HTTP 400 nếu không tìm thấy sản phẩm

            return Ok(product); // Trả về HTTP 200 với danh sách sản phẩm
        }
        [HttpGet("search/{name}")]
        public async Task<IActionResult> UpdateProdcutAsync(string name)
        {
            var product = await _productService.GetProductByNameAsync(name);
            if (!product.Success)
                return BadRequest(product); // Trả về HTTP 400 nếu không tìm thấy sản phẩm

            return Ok(product); // Trả về HTTP 200 với danh sách sản phẩm
        }
        [Authorize(Policy = nameof(Enums.Role.Manager))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProdcutAsync(int id)
        {
          
            var product = await _productService.DeleteProductAsync(id);
            if (!product.Success)
                return BadRequest(product); // Trả về HTTP 400 nếu không tìm thấy sản phẩm

            return Ok(product); // Trả về HTTP 200 với danh sách sản phẩm
        }
        [HttpGet("search")]
        public async Task<IActionResult> SearchProductAsync([FromBody] ProductSearchRequestDto searchProductDto)
        {
            var product = await _productService.SearchProductAsync(searchProductDto);
            if (!product.Success)
                return BadRequest(product); // Trả về HTTP 400 nếu không tìm thấy sản phẩm
            return Ok(product); // Trả về HTTP 200 với danh sách sản phẩm
        }
    }
}

using Application__CaféManagementSystem.Application_.DTOs.Products;
using Application__CaféManagementSystem.Application_.Interface;
using Application__CaféManagementSystem.Application_.Models;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CoffeeManagementSystem.Controllers
{
    [ApiController]
    [Route("api/Controller")]
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
        [Authorize] // Chỉ yêu cầu đăng nhập, không kiểm tra role
        [HttpPost("add")]
        public async Task<IActionResult> AddProduct(CreateProductDto productDto)
        {
            var roleId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            if (roleId == null)
            {
                return Forbid(); // Trả về 403 nếu không có quyền
            }
            var roleName = await _authService.GetRoleNameByIdAsync(int.Parse(roleId));
            if (roleName.Equals("Admin") && roleName.Equals("Quản lý"))
            {
                return Forbid(); // Trả về 403 nếu không có quyền
            }
            if (productDto == null)
            {
                return BadRequest("Dữ liệu đầu vào không hợp lệ.");
            }

            var product = await _productService.AddProductAsync(productDto);

            if (!product.Success)
            {
                return BadRequest(product);
            }

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
        [Authorize]
        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> UpdateProdcutAsync(UpdateProductDto productDto)
        {
            var roleId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            if (roleId == null) 
                return Forbid(); // Trả về 403 nếu không có quyền
            var roleName = await _authService.GetRoleNameByIdAsync(int.Parse(roleId));
            if (roleName.Equals("Admin") && roleName.Equals("Quản lý"))
                return Forbid(); // Trả về 403 nếu không có quyền

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
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProdcutAsync(int id)
        {
            var roleId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            if (roleId == null)
                return Forbid(); // Trả về 403 nếu không có quyền
            var roleName = await _authService.GetRoleNameByIdAsync(int.Parse(roleId));
            if (roleName.Equals("Admin") && roleName.Equals("Quản lý"))
                return Forbid(); // Trả về 403 nếu không có quyền

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

using Application__CaféManagementSystem.Application_.DTOs.Products;
using Application__CaféManagementSystem.Application_.Interface;
using Application__CaféManagementSystem.Application_.Models;
using Azure;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CoffeeManagementSystem.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(CreateProductDto productDto)
        {
            if (productDto == null)
            {
                return BadRequest("Dữ liệu đầu vào không hợp lệ.");
            }
            var product = await _productService.AddProductAsync(productDto);
            if (!product.Success)
            {
                return BadRequest(product); // Nếu thất bại, trả về 400 với thông báo lỗi
            }
            return Ok(product);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProduct()
        {
            var productList = await _productService.GetAllProductsAsync();

            if (!productList.Success)
                return BadRequest(productList); // Trả về HTTP 400 nếu không tìm thấy sản phẩm

            return Ok(productList); // Trả về HTTP 200 với danh sách sản phẩm
        }
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
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProdcutAsync(int id)
        {
            var product = await _productService.DeleteProductAsync(id);
            if (!product.Success)
                return BadRequest(product); // Trả về HTTP 400 nếu không tìm thấy sản phẩm

            return Ok(product); // Trả về HTTP 200 với danh sách sản phẩm
        }
    }
}

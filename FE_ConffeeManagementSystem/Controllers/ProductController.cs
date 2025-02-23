using Microsoft.AspNetCore.Mvc;

namespace FE_ConffeeManagementSystem.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

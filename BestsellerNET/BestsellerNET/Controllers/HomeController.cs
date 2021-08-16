using Microsoft.AspNetCore.Mvc;
using BestsellerNET.Services;

namespace BestsellerNET.Controllers
{
    public class HomeController : Controller
    {
        private readonly JsonProductService productService;
        public HomeController(JsonProductService jsonProductService)
        {
            productService = jsonProductService;
        }
        public IActionResult Index()
        {
            return View(productService.GetProducts());
        }
        public IActionResult Privacy()
        {
            return View();
        }
    }
}

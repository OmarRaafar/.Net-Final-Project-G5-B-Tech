using ApplicationB.Services_B.General;
using ApplicationB.Services_B.Product;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminDashboardB.Controllers
{
    public class ProductController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IProductService productService;
        private readonly ILanguageService languageService;

        public ProductController(IProductService _productService, IWebHostEnvironment _webHostEnvironment,
            ILanguageService _languageService)
        {
            productService = _productService;
            webHostEnvironment = _webHostEnvironment;
            languageService = _languageService;
        }
        public async Task<IActionResult> Index()
        {
            //languageService.GetCurrentLanguageCode();
            return View(await productService.GetAllProductsAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productB = await productService.GetProductByIdAsync(id);

            return View(productB);
        }

        public async Task<IActionResult> Search(string searchString)
        {
            var products = await productService.SearchProductsByNameAsync(searchString); 
            ViewBag.SearchString = searchString;

            return View("Index", products);
        }
    }
}

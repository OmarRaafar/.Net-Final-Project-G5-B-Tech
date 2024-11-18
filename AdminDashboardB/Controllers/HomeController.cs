using ApplicationB.Services_B.Category;
using ApplicationB.Services_B.Order;
using ApplicationB.Services_B.Product;
using ApplicationB.Services_B.User;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService productService;
        private readonly ICategoryService CatService;
        private readonly IOrderService OrderService;
        private readonly IUserService UserService;


        public HomeController(ILogger<HomeController> logger, IProductService _productService, ICategoryService _catService, IOrderService _orderService, IUserService _userService)
        {
            _logger = logger;
            productService = _productService;
            CatService = _catService;
            OrderService = _orderService;
            UserService = _userService;
        }

        public IActionResult Index()
        {
            ViewBag.ProductsCount = productService.GetAllProductsAsync().Result?.Entity?.Count() ?? 0;
            ViewBag.CategoriesCount = CatService.GetAllCategoriesAsync().Result?.Entity?.Count() ?? 0;
            ViewBag.OrdersCount = OrderService.GetAllOrdersAsync().Result?.Count() ?? 0;
            ViewBag.UsersCount = UserService.GetAllAppUsersAsync().Result?.Count() ?? 0;


            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

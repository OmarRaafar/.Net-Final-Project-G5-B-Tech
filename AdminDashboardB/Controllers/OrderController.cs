using Microsoft.AspNetCore.Mvc;

namespace AdminDashboardB.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

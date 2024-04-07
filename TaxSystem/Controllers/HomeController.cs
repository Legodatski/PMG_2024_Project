using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TaxSystem.Models;

namespace TaxSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

         public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return RedirectToAction("All", "Amenity");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            if (TempData["Error"] != null)
            {
                string errorMsg = TempData["Error"].ToString();
                ViewBag.Error = errorMsg;
            }
            return View();
        }
    }
}

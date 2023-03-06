using Microsoft.AspNetCore.Mvc;
using SobKichuBookStore.DataAccess.Repository.IRepository;
using SobKichuBookStore.Models;
using System.Diagnostics;

namespace SobKichuBookStore.Web.Areas.Customer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork unit;

        public HomeController(ILogger<HomeController> logger,IUnitOfWork _unit)
        {
            _logger = logger;
            unit= _unit;  
        }

        public IActionResult Index()
        {
            IEnumerable<Product> products=unit.ProductRepo.GetAll(includeProperties:"category");
            return View(products);
        }
        public IActionResult Details(int productId)
        {
			ShoppingCart shoppingCart = new()
			{
				Count = 1,
				product = unit.ProductRepo.GetFisrtOrDefault(u => u.Id == productId,includeProperties:"category")
		     };
            return View(shoppingCart);
            //return Json(new { data = shoppingCart });
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
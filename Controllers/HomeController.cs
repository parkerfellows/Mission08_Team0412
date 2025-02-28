using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Mission08_Team0412.Models;

namespace Mission08_Team0412.Controllers
{
    public class HomeController : Controller
    {

        private readonly TaskContext _context;

        public HomeController(TaskContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult QuadrantView()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult QuadrantView()
        {
            return View();
        }
    }
}

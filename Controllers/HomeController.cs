using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Mission08_Team0412.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Mission08_Team0412.Controllers
{
    public class HomeController : Controller
    {

        private readonly TaskDbContext _context;

        public HomeController(TaskDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            var tasks = _context.Tasks
                .Where(t => t.Completed == false)
                .ToList();
            return View(tasks);
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
            ViewBag.Categories = _context.Categories.ToList();
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            // Get categories for dropdown
            ViewBag.Categories = _context.Categories.ToList();

            // Get the task to edit
            var taskToEdit = _context.Tasks
                .Single(t => t.TaskId == id);

            return View("AddTask", taskToEdit);
        }

        [HttpPost]
        public IActionResult Edit(TaskItem task)
        {
            if (ModelState.IsValid)
            {
                _context.Update(task);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                // If validation fails, repopulate categories and return to form
                ViewBag.Categories = _context.Categories.ToList();
                return View("AddTask", task);
            }
        }

        public IActionResult QuadrantView()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create( TaskItem task)
        {
            if (ModelState.IsValid)
            {
                _context.Add(task);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                // If validation fails, repopulate categories and return to form
                ViewBag.Categories = _context.Categories.ToList();
                return View(task);
            }
            
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var task = _context.Tasks.Single(t => t.TaskId == id);
            _context.Tasks.Remove(task);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

     
    }
}

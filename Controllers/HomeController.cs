using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Mission08_Team0412.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            var categories = _context.Categories
                .Select(c => new SelectListItem
                {
                    Value = c.CategoryId.ToString(),
                    Text = c.CategoryName
                })
                .ToList();

            if (!categories.Any()) // Debugging: Check if categories exist
            {
                Console.WriteLine("No categories found in database.");
            }

            ViewBag.Categories = categories; // Ensure this is not null

            return View();
        }



        public IActionResult Quadrants()
        {
            var tasks = _context.Tasks.ToList(); // Retrieve tasks from the database

            if (tasks == null)
            {
                tasks = new List<TaskItem>(); // Ensure Model is never null
            }

            return View(tasks); // Pass tasks to the view
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            // Convert categories to SelectListItem before passing to ViewBag
            ViewBag.Categories = _context.Categories
                .Select(c => new SelectListItem
                {
                    Value = c.CategoryId.ToString(),
                    Text = c.CategoryName
                })
                .ToList();

            // Fetch the task to edit
            var taskToEdit = _context.Tasks
                .SingleOrDefault(t => t.TaskId == id);

            if (taskToEdit == null)
            {
                return NotFound();
            }

            return View("AddTask", taskToEdit);
        }

        [HttpPost]
        public IActionResult Edit(TaskItem task)
        {
            if (ModelState.IsValid)
            {
                _context.UpdateTask(task);
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
                _context.AddTask(task);
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
            _context.DeleteTask(task);

            return RedirectToAction("Index");
        }

    }
}

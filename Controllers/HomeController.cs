using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Mission08_Team0412.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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
            var tasks = _context.Tasks
                .Include(t => t.Category) // Ensure Category is loaded
                .ToList() ?? new List<TaskItem>(); // Ensure list is never null

            return View(tasks);
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            // Convert categories to SelectListItem before passing to ViewBag
            ViewBag.Categories = _context.Categories
                .ToList();

            // Fetch the task to edit
            var taskToEdit = _context.Tasks
                .SingleOrDefault(t => t.TaskId == id);

            if (taskToEdit == null)
            {
                return NotFound();
            }

            return View("EditTask", taskToEdit);
        }

        [HttpPost]
        public IActionResult Edit(TaskItem task)
        {

            _context.Update(task);
            _context.SaveChanges();
            return RedirectToAction("Quadrants");
        }
                
                


        public IActionResult QuadrantView()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create( TaskItem task)
        {
     
            _context.AddTask(task);
            return RedirectToAction("Index");
            
   
            
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var task = _context.Tasks.Find(id);

            if (task == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(task);
            _context.SaveChanges();

            return RedirectToAction("Quadrants");
        }

        [HttpPost]
        public async Task<IActionResult> MarkCompleted(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            task.Completed = true; // Mark task as completed
            await _context.SaveChangesAsync(); // Save changes to the database
            return RedirectToAction("Quadrants"); // Redirect to the Quadrants view
        }
    }
}

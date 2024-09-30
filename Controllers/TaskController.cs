using Microsoft.AspNetCore.Mvc;
using TaskManagementApp.Models;

namespace TaskManagementApp.Controllers
{
    public class TaskController : Controller
    {
        private static List<TaskModel> tasks = new List<TaskModel>();

        public IActionResult Index()
        {
            return View(tasks);
        }

        // Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TaskModel task)
        {
            if (ModelState.IsValid)
            {
                task.Id = tasks.Count > 0 ? tasks.Max(t => t.Id) + 1 : 1;
                tasks.Add(task);
                return RedirectToAction(nameof(Index));
            }
            return View(task);
        }

        // Edit
        public IActionResult Edit(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                return NotFound("Task not found.");
            }
            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TaskModel task)
        {
            if (ModelState.IsValid)
            {
                var existingTask = tasks.FirstOrDefault(t => t.Id == task.Id);
                if (existingTask == null)
                {
                    return NotFound("Task not found.");
                }
                existingTask.Title = task.Title;
                existingTask.Description = task.Description;
                existingTask.Deadline = task.Deadline;
                return RedirectToAction(nameof(Index));
            }
            return View(task);
        }

        // Delete
        public IActionResult Delete(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                return NotFound("Task not found.");
            }
            return View(task);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                return NotFound("Task not found.");
            }
            tasks.Remove(task);
            return RedirectToAction(nameof(Index));
        }
    }
}

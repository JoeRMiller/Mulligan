using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mulligan.Core.Data;

namespace Mulligan.Web.Controllers
{
    public class DashboardController : Controller
    {
        private readonly CoreDbContext _context;

        public DashboardController(CoreDbContext context)
        {
            _context = context;
        }
        // GET: Dashboard
        [Authorize]
        [Route("dashboard")]
        public ActionResult Index()
        {
            var facilities = _context.Facilities.Take(10).ToList(); // Assuming you have a DbSet<Facility> Facilities in CoreDbContext
            return View(facilities);
        }

        [Authorize]
        [HttpGet("Search")]
        public IActionResult Search(string searchName)
        {
            var facilities = string.IsNullOrWhiteSpace(searchName)
                ? _context.Facilities.ToList()
                : _context.Facilities.Include(f => f.Courses)
                                     .Where(f => f.Name.ToLower().Contains(searchName.ToLower()))
                                     .ToList();
            return View("Index", facilities); // Reuse the Index view for displaying results
        }


        // GET: Dashboard/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Dashboard/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dashboard/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Dashboard/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Dashboard/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Dashboard/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Dashboard/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

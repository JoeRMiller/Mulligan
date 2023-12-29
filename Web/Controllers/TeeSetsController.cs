using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mulligan.Core.Data;
using Mulligan.Core.Models;

namespace Mulligan.Web.Controllers
{
    public class TeeSetsController : Controller
    {
        private readonly CoreDbContext _context;

        public TeeSetsController(CoreDbContext context)
        {
            _context = context;
        }

        // GET: TeeSets
        public async Task<IActionResult> Index()
        {
            var coreDbContext = _context.Tees.Include(t => t.Course);
            return View(await coreDbContext.ToListAsync());
        }

        // GET: TeeSets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teeSet = await _context.Tees
                .Include(t => t.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teeSet == null)
            {
                return NotFound();
            }

            return View(teeSet);
        }

        // GET: TeeSets/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Courses, "NCRDId", "Name");
            return View();
        }

        // POST: TeeSets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CourseId,Name,Gender,Par,CourseRating,BogeyRating,Slope,FrontRating,FrontSlope,BackRating,BackSlope")] TeeSet teeSet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teeSet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "NCRDId", "Name", teeSet.CourseId);
            return View(teeSet);
        }

        // GET: TeeSets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teeSet = await _context.Tees.FindAsync(id);
            if (teeSet == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "NCRDId", "Name", teeSet.CourseId);
            return View(teeSet);
        }

        // POST: TeeSets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CourseId,Name,Gender,Par,CourseRating,BogeyRating,Slope,FrontRating,FrontSlope,BackRating,BackSlope")] TeeSet teeSet)
        {
            if (id != teeSet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teeSet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeeSetExists(teeSet.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "NCRDId", "Name", teeSet.CourseId);
            return View(teeSet);
        }

        // GET: TeeSets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teeSet = await _context.Tees
                .Include(t => t.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teeSet == null)
            {
                return NotFound();
            }

            return View(teeSet);
        }

        // POST: TeeSets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teeSet = await _context.Tees.FindAsync(id);
            if (teeSet != null)
            {
                _context.Tees.Remove(teeSet);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeeSetExists(int id)
        {
            return _context.Tees.Any(e => e.Id == id);
        }
    }
}

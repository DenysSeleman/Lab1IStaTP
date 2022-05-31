using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab1_Scientists_And_Publications;

namespace Lab1_Scientists_And_Publications.Controllers
{
    public class ScientistsController : Controller
    {
        private readonly Lab1_ScientistsAndPublicationsContext _context;

        public ScientistsController(Lab1_ScientistsAndPublicationsContext context)
        {
            _context = context;
        }

        // GET: Scientists
        public async Task<IActionResult> Index()
        {
            var lab1_ScientistsAndPublicationsContext = _context.Scientists.Include(s => s.Department);
            return View(await lab1_ScientistsAndPublicationsContext.ToListAsync());
        }



        public async Task<IActionResult> IndexOfOneEdition(int? id, string? name, string? title)
        {
            if (id == null) return RedirectToAction("Editions", "Index");

            ViewBag.EditionId = id;
            ViewBag.EditionName = name;
            ViewBag.PublicationTitle = title;

            var scientistsOfEdition = _context.ScientistEditions.Where(b => b.EditionId == id).Select(b => b.Scientist);//.Include(b => b.Department);
            //var scientistsOfEdition = from b in _context.ScientistEditions where(b.EditionId == id) select b.Scientist;

            return View(await scientistsOfEdition.ToListAsync());
        }



        // GET: Scientists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Scientists == null)
            {
                return NotFound();
            }

            var scientist = await _context.Scientists
                .Include(s => s.Department)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (scientist == null)
            {
                return NotFound();
            }

            //return View(scientist);
            return RedirectToAction("IndexOfOneScientist", "Editions", new { id = scientist.Id, fullName = scientist.FullName });
        }

        // GET: Scientists/Create
        public IActionResult Create()
        {
            ViewBag.Departments = new SelectList(_context.Departments, "Id", "Name");
            //ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Id");
            return View();
        }

        // POST: Scientists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FullName,DateOfBirth,ScienceDegree,DepartmentId")] Scientist scientist)
        {
            if (ModelState.IsValid)
            {
                _context.Add(scientist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Id", scientist.DepartmentId);
            return View(scientist);
        }

        // GET: Scientists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Scientists == null)
            {
                return NotFound();
            }

            var scientist = await _context.Scientists.FindAsync(id);
            if (scientist == null)
            {
                return NotFound();
            }

            ViewBag.Departments = new SelectList(_context.Departments, "Id", "Name");

            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Id", scientist.DepartmentId);
            return View(scientist);
        }

        // POST: Scientists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,DateOfBirth,ScienceDegree,DepartmentId")] Scientist scientist)
        {
            if (id != scientist.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(scientist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScientistExists(scientist.Id))
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
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Id", scientist.DepartmentId);
            return View(scientist);
        }

        // GET: Scientists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Scientists == null)
            {
                return NotFound();
            }

            var scientist = await _context.Scientists
                .Include(s => s.Department)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (scientist == null)
            {
                return NotFound();
            }

            return View(scientist);
        }

        // POST: Scientists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Scientists == null)
            {
                return Problem("Entity set 'Lab1_ScientistsAndPublicationsContext.Scientists'  is null.");
            }
            var scientist = await _context.Scientists.FindAsync(id);
            if (scientist != null)
            {
                _context.Scientists.Remove(scientist);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ScientistExists(int id)
        {
          return (_context.Scientists?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

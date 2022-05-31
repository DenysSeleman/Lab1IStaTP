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
    public class EditionsController : Controller
    {
        private readonly Lab1_ScientistsAndPublicationsContext _context;

        public EditionsController(Lab1_ScientistsAndPublicationsContext context)
        {
            _context = context;
        }

        // GET: Editions
        public async Task<IActionResult> IndexOfOnePublication(int? id, string? title)
        {
            if (id == null) return RedirectToAction("Publications", "Index");

            ViewBag.PublicationId = id;
            ViewBag.PublicationTitle = title;
            var editionsOfPublication = _context.Editions.Where(b => b.PublicationId == id).Include(b => b.Publication);

            return View(await editionsOfPublication.ToListAsync());
        }
        public async Task<IActionResult> Index()
        {
            var lab1_ScientistsAndPublicationsContext = _context.Editions.Include(e => e.Publication);
            return View(await lab1_ScientistsAndPublicationsContext.ToListAsync());
        }
        public async Task<IActionResult> IndexOfOneScientist(int? id, string? fullName)
        {
            if (id == null) return RedirectToAction("Scientists", "Index");

            ViewBag.ScientistId = id;
            ViewBag.ScientistFullName = fullName;

            var editionsOfScientist = (from b in _context.ScientistEditions where (b.ScientistId == id) select b.Edition);//.Include(a => a.Publication);

            return View(await editionsOfScientist.ToListAsync());
        }

        // GET: Editions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Editions == null)
            {
                return NotFound();
            }

            var edition = await _context.Editions
                .Include(e => e.Publication)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (edition == null)
            {
                return NotFound();
            }
            var title = from a in _context.Publications where a.Id == edition.PublicationId select a.Title;
            //return View(edition);
            return RedirectToAction("IndexOfOneEdition", "Scientists", new { id = edition.Id, name = edition.Name, title });
        }

        // GET: Editions/Create
        public IActionResult Create(int publicationId)
        {
            //ViewData["PublicationId"] = new SelectList(_context.Publications, "Id", "Title");
            ViewBag.PublicationId = publicationId;
            ViewBag.PublicationTitle = _context.Publications.Where(c => c.Id == publicationId).FirstOrDefault().Title;
            return View();
        }

        public IActionResult CreateCommon()
        {
            ViewBag.Publications = new SelectList(_context.Publications, "Id", "Title");
            //ViewData["PublicationId"] = new SelectList(_context.Publications, "Id", "Title");
            return View();
        }


        // POST: Editions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]


        public async Task<IActionResult> Create(int publicationId, [Bind("Id,Name,NumberOfPages,ReleaseDate")] Edition edition)
        {
            edition.PublicationId = publicationId;
            edition.Publication = _context.Publications.Where(c => c.Id == publicationId).FirstOrDefault();
            //if (ModelState.IsValid)
            //{
                _context.Add(edition);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("IndexOfOnePublication", "Editions", new { id = publicationId, title = _context.Publications.Where(c => c.Id == publicationId).FirstOrDefault().Title });
            //}
            //ViewData["PublicationId"] = new SelectList(_context.Publications, "Id", "Title", edition.PublicationId);
            //return View(edition);
            //return RedirectToAction("IndexOfOnePublication", "Editions", new { id = publicationId, title = _context.Publications.Where(c => c.Id == publicationId).FirstOrDefault().Title });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCommon([Bind("Id,Name,NumberOfPages,ReleaseDate,PublicationId")] Edition edition)
        {
            edition.Publication = _context.Publications.Where(c => c.Id == edition.PublicationId).FirstOrDefault();
            /*ModelState.ClearValidationState(nameof(edition));
            if (!TryValidateModel(edition, nameof(Edition)))
            {
                _context.Add(edition);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }*/
            if (ModelState.IsValid)
            {
                _context.Add(edition);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PublicationId"] = new SelectList(_context.Publications, "Id", "Id", edition.PublicationId);

            //return RedirectToAction(nameof(Index));
            //return RedirectToAction("IndexOfOnePublication", "Editions");
            return View("CreateCommon", edition);
        }
        


        // GET: Editions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Editions == null)
            {
                return NotFound();
            }

            var edition = await _context.Editions.FindAsync(id);
            if (edition == null)
            {
                return NotFound();
            }
            ViewData["PublicationId"] = new SelectList(_context.Publications, "Id", "Title", edition.PublicationId);
            return View(edition);
        }

        // POST: Editions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,NumberOfPages,ReleaseDate,PublicationId")] Edition edition)
        {
            if (id != edition.Id)
            {
                return NotFound();
            }
            edition.Publication = _context.Publications.Where(c => c.Id == edition.PublicationId).FirstOrDefault();

            //ModelState.ClearValidationState(nameof(Publication));
            /*if (!TryValidateModel(Index, nameof(Index)))
            {
                return Page();
            }*/

            //if (ModelState.IsValid)
            //{
                try
                {
                    _context.Update(edition);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EditionExists(edition.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            //}
            //ViewData["PublicationId"] = new SelectList(_context.Publications, "Id", "Title", edition.PublicationId);
            //return View(edition);
        }

        // GET: Editions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Editions == null)
            {
                return NotFound();
            }

            var edition = await _context.Editions
                .Include(e => e.Publication)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (edition == null)
            {
                return NotFound();
            }

            return View(edition);
        }

        // POST: Editions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Editions == null)
            {
                return Problem("Entity set 'Lab1_ScientistsAndPublicationsContext.Editions'  is null.");
            }
            var edition = await _context.Editions.FindAsync(id);
            if (edition != null)
            {
                _context.Editions.Remove(edition);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EditionExists(int id)
        {
          return (_context.Editions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

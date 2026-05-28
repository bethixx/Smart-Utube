using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Smart_Utube.Data;
using Smart_Utube.Models;

namespace Smart_Utube.Controllers
{
    public class ExternalDescriptionsController : Controller
    {
        private readonly AppDbContext _context;

        public ExternalDescriptionsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ExternalDescriptions
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ExternalDescriptions.Include(e => e.Movie);
            return View(await appDbContext.ToListAsync());
        }

        // GET: ExternalDescriptions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var externalDescription = await _context.ExternalDescriptions
                .Include(e => e.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (externalDescription == null)
            {
                return NotFound();
            }

            return View(externalDescription);
        }

        // GET: ExternalDescriptions/Create
        public IActionResult Create()
        {
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Id");
            return View();
        }

        // POST: ExternalDescriptions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MovieId,Source,GeneratedText,CreatedAt")] ExternalDescription externalDescription)
        {
            if (ModelState.IsValid)
            {
                _context.Add(externalDescription);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Id", externalDescription.MovieId);
            return View(externalDescription);
        }

        // GET: ExternalDescriptions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var externalDescription = await _context.ExternalDescriptions.FindAsync(id);
            if (externalDescription == null)
            {
                return NotFound();
            }
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Id", externalDescription.MovieId);
            return View(externalDescription);
        }

        // POST: ExternalDescriptions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MovieId,Source,GeneratedText,CreatedAt")] ExternalDescription externalDescription)
        {
            if (id != externalDescription.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(externalDescription);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExternalDescriptionExists(externalDescription.Id))
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
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Id", externalDescription.MovieId);
            return View(externalDescription);
        }

        // GET: ExternalDescriptions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var externalDescription = await _context.ExternalDescriptions
                .Include(e => e.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (externalDescription == null)
            {
                return NotFound();
            }

            return View(externalDescription);
        }

        // POST: ExternalDescriptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var externalDescription = await _context.ExternalDescriptions.FindAsync(id);
            if (externalDescription != null)
            {
                _context.ExternalDescriptions.Remove(externalDescription);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExternalDescriptionExists(int id)
        {
            return _context.ExternalDescriptions.Any(e => e.Id == id);
        }
    }
}

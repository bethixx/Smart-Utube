using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Smart_Utube.Data;
using System.Security.Claims;

public class WatchHistoriesController : Controller
{
    private readonly AppDbContext _context;

    public WatchHistoriesController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var history = await _context.WatchHistories
            .Where(w => w.UserId == userId)
            .Include(w => w.Movie)
            .OrderByDescending(w => w.WatchedAt)
            .ToListAsync();

        return View(history);
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var history = await _context.WatchHistories
            .FirstOrDefaultAsync(h => h.Id == id);

        if (history == null)
            return NotFound();

        if (history.UserId != userId && !User.IsInRole("Admin"))
            return Forbid();

        _context.WatchHistories.Remove(history);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ClearAll()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var history = _context.WatchHistories
            .Where(h => h.UserId == userId);

        _context.WatchHistories.RemoveRange(history);

        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }
}
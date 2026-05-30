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
}
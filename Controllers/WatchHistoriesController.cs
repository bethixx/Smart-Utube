using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Smart_Utube.Data;

namespace Smart_Utube.Controllers
{
    public class WatchHistoryController : Controller
    {
        private readonly AppDbContext _context;

        public WatchHistoryController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var history = await _context.WatchHistories
                .Include(w => w.Movie)
                .OrderByDescending(w => w.WatchedAt)
                .ToListAsync();

            return View(history);
        }
    }
}

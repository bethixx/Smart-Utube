using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Smart_Utube.Models;
using Smart_Utube.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Smart_Utube.Controllers
{
    [Authorize]
    public class FavoritesController : Controller
    {
        private readonly AppDbContext _context;

        public FavoritesController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var favorites = await _context.Favorites
                .Where(f => f.UserId == userId)
                .Include(f => f.Movie)
                .ToListAsync();

            return View(favorites);
        }

        public async Task<IActionResult> Add(int movieId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var exists = await _context.Favorites
                .AnyAsync(f => f.UserId == userId && f.MovieId == movieId);

            if (!exists)
            {
                _context.Favorites.Add(new Favorite
                {
                    UserId = userId,
                    MovieId = movieId
                });

                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Movies");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(int movieId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var favorite = await _context.Favorites
                .FirstOrDefaultAsync(f => f.UserId == userId && f.MovieId == movieId);

            if (favorite != null)
            {
                _context.Favorites.Remove(favorite);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

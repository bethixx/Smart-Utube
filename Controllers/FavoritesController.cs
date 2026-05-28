using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Smart_Utube.Models;
using Smart_Utube.Data;

namespace Smart_Utube.Controllers
{
    public class FavoritesController : Controller
    {
        private readonly AppDbContext _context;

        public FavoritesController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Add(int movieId, int userId)
        {
            var favorite = new Favorite
            {
                MovieId = movieId,
                UserId = userId
            };

            _context.Favorites.Add(favorite);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Movies");
        }

        public async Task<IActionResult> Remove(int movieId, int userId)
        {
            var favorite = await _context.Favorites
                .FindAsync(userId, movieId);

            if (favorite != null)
            {
                _context.Favorites.Remove(favorite);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Movies");
        }
    }
}

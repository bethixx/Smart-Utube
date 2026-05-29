using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Smart_Utube.Data;
using Smart_Utube.Models;
using System.Security.Claims;

namespace Smart_Utube.Controllers
{
    public class RatingsController : Controller
    {
        private readonly AppDbContext _context;

        public RatingsController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize]
        // ADD OR UPDATE RATING
        [HttpPost]
        public async Task<IActionResult> Rate(int movieId, int value)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var existingRating = await _context.Ratings
                .FirstOrDefaultAsync(r =>
                    r.MovieId == movieId &&
                    r.UserId == userId);

            if (existingRating != null)
            {
                // update existing rating
                existingRating.Value = value;
            }
            else
            {
                // create new rating
                var rating = new Rating
                {
                    MovieId = movieId,
                    UserId = userId,
                    Value = value
                };

                _context.Ratings.Add(rating);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Movies", new { id = movieId });
        }

        // DELETE RATING
        [Authorize]
        public async Task<IActionResult> Delete(int movieId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var rating = await _context.Ratings
                .FirstOrDefaultAsync(r =>
                    r.MovieId == movieId &&
                    r.UserId == userId);

            if (rating != null)
            {
                _context.Ratings.Remove(rating);

                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Details", "Movies", new { id = movieId });
        }
    }
}
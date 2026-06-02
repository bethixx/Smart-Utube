using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Smart_Utube.DTOs.Movie;
using Smart_Utube.Models;
using Smart_Utube.Services;
using Smart_Utube.Data;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Smart_Utube.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly AppDbContext _context;

        public MoviesController(IMovieService movieService, AppDbContext context)
        {
            _movieService = movieService;
            _context = context;
        }

        // GET: Movies
        public async Task<IActionResult> Index(string searchString, int? categoryId)
        {
            var movies = await _movieService.GetAllAsync(searchString, categoryId);

            if (User.Identity?.IsAuthenticated == true)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                ViewBag.Playlists = await _context.Playlists
                    .Where(p => p.UserId == userId)
                    .ToListAsync();
            }

            return View(movies);
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var movie = await _movieService.GetByIdAsync(id.Value);

            if (movie is null) return NotFound();

            return View(movie);
        }

        // GET: Movies/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewBag.Categories = new MultiSelectList(
                _context.Categories,
                "Id",
                "Name"
            );

            return View();
        }

        // POST: Movies/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MovieCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new MultiSelectList(
                    _context.Categories,
                    "Id",
                    "Name"
                );

                return View(dto);
            }

            await _movieService.CreateAsync(dto);

            return RedirectToAction(nameof(Index));
        }

        // GET: Movies/Edit
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var movie = await _movieService.GetByIdAsync(id.Value);

            if (movie is null)
                return NotFound();

            ViewBag.Categories = new MultiSelectList(
                _context.Categories,
                "Id",
                "Name",
                movie.CategoryIds
            );

            var dto = new MovieUpdateDto
            {
                Id = movie.Id,
                Title = movie.Title,
                YouTubeUrl = movie.YouTubeUrl,
                Description = movie.Description,
                Duration = movie.Duration,
                CreatedAt = movie.CreatedAt,
                CategoryIds = movie.CategoryIds
            };

            return View(dto);
        }

        // POST: Movies/Edit
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MovieUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            await _movieService.UpdateAsync(dto);

            return RedirectToAction(nameof(Index));
        }

        // GET: Movies/Delete
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var movie = await _movieService.GetByIdAsync(id.Value);

            if (movie is null) return NotFound();

            return View(movie);
        }

        // POST: Movies/Delete
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _movieService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public async Task<IActionResult> Watch(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var movie = await _movieService.GetByIdAsync(id);
            if (movie == null) return NotFound();

            _context.WatchHistories.Add(new WatchHistory
            {
                MovieId = id,
                UserId = userId!,
                WatchedAt = DateTime.Now
            });

            await _context.SaveChangesAsync();

            return Redirect(movie.YouTubeUrl);
        }
    }
}

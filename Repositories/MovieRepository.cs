using Smart_Utube.Data;
using Smart_Utube.Models;
using Microsoft.EntityFrameworkCore;

namespace Smart_Utube.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly AppDbContext _context;

        public MovieRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Movie>> GetAllAsync()
        {
            return await _context.Movies
                .Include(m => m.Ratings)
                .Include(m => m.MovieCategories)
                    .ThenInclude(mc => mc.Category)
                .ToListAsync();
        }

        public async Task<Movie?> GetByIdAsync(int id)
        {
            return await _context.Movies
                .Include(m => m.Ratings)
                .Include(m => m.Comments)
                    .ThenInclude(c => c.User)
                .Include(m => m.MovieCategories)
                    .ThenInclude(mc => mc.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task AddAsync(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Movie movie)
        {
            _context.Movies.Update(movie);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie != null)
            {
                _context.Movies.Remove(movie);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateWithCategoriesAsync(Movie movie, List<int> categoryIds)
        {
            var existing = await _context.Movies
                .Include(m => m.MovieCategories)
                .FirstOrDefaultAsync(m => m.Id == movie.Id);

            if (existing == null) return;

            existing.Title = movie.Title;
            existing.YouTubeUrl = movie.YouTubeUrl;
            existing.Description = movie.Description;
            existing.Duration = movie.Duration;
            existing.CreatedAt = movie.CreatedAt;

            _context.MovieCategories.RemoveRange(existing.MovieCategories);

            existing.MovieCategories = categoryIds.Select(id => new MovieCategory
            {
                MovieId = movie.Id,
                CategoryId = id
            }).ToList();

            await _context.SaveChangesAsync();
        }
    }
}

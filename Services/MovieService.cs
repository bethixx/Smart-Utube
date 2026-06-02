using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Smart_Utube.DTOs.Movie;
using Smart_Utube.Models;
using Smart_Utube.Repositories;

namespace Smart_Utube.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _repository;

        public MovieService(IMovieRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<MovieReadDto>> GetAllAsync(string? searchString, int? categoryId)
        {
            var movies = await _repository.GetAllAsync();

            if (!string.IsNullOrEmpty(searchString))
            {
                movies = movies
                    .Where(m => m.Title.ToLower().Contains(searchString.ToLower()))
                    .ToList();
            }

            if (categoryId.HasValue)
            {
                movies = movies
                    .Where(m => m.MovieCategories.Any(mc => mc.CategoryId == categoryId))
                    .ToList();
            }

            return movies.Select(m => new MovieReadDto
            {
                Id = m.Id,
                Title = m.Title,
                YouTubeUrl = m.YouTubeUrl,
                Description = m.Description,
                Duration = m.Duration,
                CreatedAt = m.CreatedAt,
                AverageRating = m.Ratings.Any()
                    ? m.Ratings.Average(r => r.Value)
                    : 0
            }).ToList();
        }

        public async Task<MovieReadDto?> GetByIdAsync(int id)
        {
            var m = await _repository.GetByIdAsync(id);

            if (m == null) return null;

            return new MovieReadDto
            {
                Id = m.Id,
                Title = m.Title,
                YouTubeUrl = m.YouTubeUrl,
                Description = m.Description,
                Duration = m.Duration,
                CreatedAt = m.CreatedAt,

                AverageRating = m.Ratings.Any()
                ? m.Ratings.Average(r => r.Value)
                : 0,

                Categories = m.MovieCategories
                    .Select(mc => mc.Category.Name)
                    .ToList(),

                Comments = m.Comments.Select(c => new CommentReadDto
                {
                    Id = c.Id,
                    Content = c.Content,
                    CreatedAt = c.CreatedAt,
                    UserNickname = c.User.Nickname,
                    UserId = c.UserId
                }).ToList(),

                CategoryIds = m.MovieCategories.Select(mc => mc.CategoryId).ToList()
            };
        }
        public async Task CreateAsync(MovieCreateDto dto)
        {
            var movie = new Movie
            {
                Title = dto.Title,
                YouTubeUrl = dto.YouTubeUrl,
                Description = dto.Description,
                Duration = dto.Duration,
                CreatedAt = dto.CreatedAt,

                MovieCategories = dto.CategoryIds.Select(id => new MovieCategory
                {
                    CategoryId = id
                }).ToList()
            };

            await _repository.AddAsync(movie);
        }

        public async Task UpdateAsync(MovieUpdateDto dto)
        {
            var movie = await _repository.GetByIdAsync(dto.Id);

            if (movie == null) return;

            movie.Title = dto.Title;
            movie.YouTubeUrl = dto.YouTubeUrl;
            movie.Description = dto.Description;
            movie.Duration = dto.Duration;
            movie.CreatedAt = dto.CreatedAt;

            await _repository.UpdateWithCategoriesAsync(movie, dto.CategoryIds);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}

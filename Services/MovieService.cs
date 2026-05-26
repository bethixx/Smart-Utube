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

        public async Task<List<MovieReadDto>> GetAllAsync()
        {
            var movies = await _repository.GetAllAsync();

            return movies.Select(m => new MovieReadDto
            {
                Id = m.Id,
                Title = m.Title,
                YouTubeUrl = m.YouTubeUrl,
                Description = m.Description,
                Duration = m.Duration,
                CreatedAt = m.CreatedAt
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
                CreatedAt = m.CreatedAt
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
                CreatedAt = DateTime.Today
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

            await _repository.UpdateAsync(movie);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}

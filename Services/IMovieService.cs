using Smart_Utube.DTOs.Movie;

namespace Smart_Utube.Services
{
    public interface IMovieService
    {
        Task<List<MovieReadDto>> GetAllAsync();
        Task<MovieReadDto?> GetByIdAsync(int id);
        Task CreateAsync(MovieCreateDto dto);
        Task UpdateAsync(MovieUpdateDto dto);
        Task DeleteAsync(int id);
    }
}

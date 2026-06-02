using Smart_Utube.Models;

namespace Smart_Utube.Repositories
{
    public interface IMovieRepository
    {
        Task<List<Movie>> GetAllAsync();
        Task<Movie?> GetByIdAsync(int id);
        Task AddAsync(Movie movie);
        Task UpdateAsync(Movie movie);
        Task DeleteAsync(int id);
        Task UpdateWithCategoriesAsync(Movie movie, List<int> categoryIds);
    }
}
